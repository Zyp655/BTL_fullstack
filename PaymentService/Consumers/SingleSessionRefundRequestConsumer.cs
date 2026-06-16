using System;
using System.Threading.Tasks;
using MassTransit;
using Contracts;
using PaymentService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PaymentService.Consumers;

public class SingleSessionRefundRequestConsumer : IConsumer<SingleSessionRefundRequestEvent>
{
    private readonly PaymentDbContext _context;
    private readonly ILogger<SingleSessionRefundRequestConsumer> _logger;

    public SingleSessionRefundRequestConsumer(PaymentDbContext context, ILogger<SingleSessionRefundRequestConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SingleSessionRefundRequestEvent> context)
    {
        var evt = context.Message;
        _logger.LogInformation("Processing SingleSessionRefundRequestEvent for StudentUserId: {StudentUserId}, ClassId: {ClassId}", evt.StudentUserId, evt.ClassId);

        try
        {
            // Find the payment invoice for this student and class
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.ClassId == evt.ClassId && p.StudentUserId == evt.StudentUserId);

            if (payment == null)
            {
                _logger.LogWarning("Payment not found for StudentUserId: {StudentUserId}, ClassId: {ClassId}", evt.StudentUserId, evt.ClassId);
                return;
            }

            if (payment.PaidAmount > 0 || payment.Status == "HoanTat")
            {
                // If they have paid, mark as BaoLuu and publish credit event
                payment.Status = "BaoLuu";
                payment.UpdatedAt = DateTime.UtcNow;
                _context.Payments.Update(payment);

                await context.Publish<StudentCreditCreatedEvent>(new StudentCreditCreatedEvent
                {
                    StudentUserId = payment.StudentUserId,
                    Amount = payment.PaidAmount,
                    SourceClassId = payment.ClassId
                });

                _logger.LogInformation("Refunded payment ID: {PaymentId} to credit wallet of {Amount} for StudentUserId: {StudentUserId}", 
                    payment.PaymentId, payment.PaidAmount, payment.StudentUserId);
            }
            else
            {
                // If they haven't paid, just cancel the payment
                payment.Status = "HuyBo";
                payment.UpdatedAt = DateTime.UtcNow;
                _context.Payments.Update(payment);
                _logger.LogInformation("Cancelled unpaid payment ID: {PaymentId} for StudentUserId: {StudentUserId}", 
                    payment.PaymentId, payment.StudentUserId);
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing SingleSessionRefundRequestEvent for StudentUserId: {StudentUserId}, ClassId: {ClassId}", evt.StudentUserId, evt.ClassId);
            throw;
        }
    }
}
