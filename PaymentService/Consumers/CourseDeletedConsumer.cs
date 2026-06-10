using System;
using System.Threading.Tasks;
using MassTransit;
using Contracts;
using PaymentService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace PaymentService.Consumers;

public class CourseDeletedConsumer : IConsumer<CourseDeletedEvent>
{
    private readonly PaymentDbContext _context;
    private readonly ILogger<CourseDeletedConsumer> _logger;

    public CourseDeletedConsumer(PaymentDbContext context, ILogger<CourseDeletedConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CourseDeletedEvent> context)
    {
        var evt = context.Message;
        _logger.LogInformation("Processing CourseDeletedEvent in PaymentService for CourseId: {CourseId}", evt.CourseId);

        try
        {
            if (evt.ClassIds == null || evt.ClassIds.Count == 0)
            {
                _logger.LogInformation("No class IDs provided for CourseId: {CourseId}. Nothing to process.", evt.CourseId);
                return;
            }

            // Find all payments for the deleted classes
            var payments = await _context.Payments
                .Where(p => evt.ClassIds.Contains(p.ClassId) && p.Status != "HuyBo" && p.Status != "BaoLuu")
                .ToListAsync();

            _logger.LogInformation("Found {Count} payments to process for CourseId: {CourseId}", payments.Count, evt.CourseId);

            foreach (var payment in payments)
            {
                if (payment.PaidAmount > 0 || payment.Status == "HoanTat")
                {
                    // Student has transferred money -> Put paid amount into wallet (BaoLuu status)
                    payment.Status = "BaoLuu";
                    payment.UpdatedAt = DateTime.UtcNow;
                    _context.Payments.Update(payment);

                    // Publish StudentCreditCreatedEvent to notify StudentService to add to credit wallet
                    await context.Publish<StudentCreditCreatedEvent>(new StudentCreditCreatedEvent
                    {
                        StudentUserId = payment.StudentUserId,
                        Amount = payment.PaidAmount,
                        SourceClassId = payment.ClassId
                    });

                    _logger.LogInformation("Payment ID: {PaymentId} marked as BaoLuu, publishing StudentCreditCreatedEvent for StudentUserId: {StudentUserId} with Amount: {Amount}", 
                        payment.PaymentId, payment.StudentUserId, payment.PaidAmount);
                }
                else
                {
                    // Student hasn't paid -> Cancel the invoice (HuyBo status)
                    payment.Status = "HuyBo";
                    payment.UpdatedAt = DateTime.UtcNow;
                    _context.Payments.Update(payment);
                    _logger.LogInformation("Payment ID: {PaymentId} (unpaid) cancelled (HuyBo) for StudentUserId: {StudentUserId}", 
                        payment.PaymentId, payment.StudentUserId);
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully processed CourseDeletedEvent in PaymentService for CourseId: {CourseId}", evt.CourseId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing CourseDeletedEvent in PaymentService for CourseId: {CourseId}", evt.CourseId);
            throw;
        }
    }
}
