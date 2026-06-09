using System;
using System.Threading.Tasks;
using MassTransit;
using Contracts;
using PaymentService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace PaymentService.Consumers;

public class ResolveCancelledClassConsumer : IConsumer<ResolveCancelledClassEvent>
{
    private readonly PaymentDbContext _context;
    private readonly ILogger<ResolveCancelledClassConsumer> _logger;

    public ResolveCancelledClassConsumer(PaymentDbContext context, ILogger<ResolveCancelledClassConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ResolveCancelledClassEvent> context)
    {
        var evt = context.Message;
        _logger.LogInformation("Processing ResolveCancelledClassEvent in PaymentService for ClassId: {ClassId}", evt.ClassId);

        try
        {
            foreach (var res in evt.Resolutions)
            {
                var payment = await _context.Payments
                    .FirstOrDefaultAsync(p => p.ClassId == evt.ClassId && p.StudentUserId == res.StudentUserId);

                if (payment != null)
                {
                    if (res.Action == "BaoLuu")
                    {
                        payment.Status = "BaoLuu";
                        payment.UpdatedAt = DateTime.UtcNow;
                        _context.Payments.Update(payment);
                        _logger.LogInformation("Updated payment ID: {PaymentId} status to BaoLuu for StudentUserId: {StudentUserId}", payment.PaymentId, res.StudentUserId);
                    }
                    else if (res.Action == "ChuyenLop")
                    {
                        if (res.NewClassId.HasValue)
                        {
                            var oldClassId = payment.ClassId;
                            payment.ClassId = res.NewClassId.Value;
                            payment.UpdatedAt = DateTime.UtcNow;
                            _context.Payments.Update(payment);
                            _logger.LogInformation("Transferred payment ID: {PaymentId} from ClassId: {OldClassId} to ClassId: {NewClassId} for StudentUserId: {StudentUserId}", payment.PaymentId, oldClassId, res.NewClassId.Value, res.StudentUserId);
                        }
                        else
                        {
                            _logger.LogWarning("ChuyenLop action received but NewClassId is null for StudentUserId: {StudentUserId}", res.StudentUserId);
                        }
                    }
                    else if (res.Action == "HoanTien")
                    {
                        payment.Status = "HoanTien";
                        payment.UpdatedAt = DateTime.UtcNow;
                        _context.Payments.Update(payment);
                        _logger.LogInformation("Updated payment ID: {PaymentId} status to HoanTien for StudentUserId: {StudentUserId}", payment.PaymentId, res.StudentUserId);
                    }
                }
                else
                {
                    _logger.LogWarning("Payment invoice not found for ClassId: {ClassId} and StudentUserId: {StudentUserId}", evt.ClassId, res.StudentUserId);
                }
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing ResolveCancelledClassEvent for ClassId: {ClassId}", evt.ClassId);
            throw;
        }
    }
}
