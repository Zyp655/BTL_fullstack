using System;
using System.Threading.Tasks;
using MassTransit;
using Contracts;
using PaymentService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace PaymentService.Consumers;

public class ClassCreatedFromQueueConsumer : IConsumer<ClassCreatedFromQueueEvent>
{
    private readonly PaymentDbContext _context;
    private readonly ILogger<ClassCreatedFromQueueConsumer> _logger;

    public ClassCreatedFromQueueConsumer(PaymentDbContext context, ILogger<ClassCreatedFromQueueConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ClassCreatedFromQueueEvent> context)
    {
        var evt = context.Message;
        _logger.LogInformation("Processing ClassCreatedFromQueueEvent in PaymentService for ClassId: {ClassId}, CourseId: {CourseId}", evt.ClassId, evt.CourseId);

        try
        {
            var targetCourseIdNegative = -evt.CourseId;

            // Find all payments for the students in this course queue where ClassId is -CourseId
            var paymentsToUpdate = await _context.Payments
                .Where(p => evt.StudentUserIds.Contains(p.StudentUserId) && p.ClassId == targetCourseIdNegative)
                .ToListAsync();

            if (paymentsToUpdate.Count > 0)
            {
                foreach (var payment in paymentsToUpdate)
                {
                    payment.ClassId = evt.ClassId;
                    payment.UpdatedAt = DateTime.UtcNow;
                    _context.Payments.Update(payment);
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully mapped {Count} waitlist payments from ClassId {NegativeId} to new ClassId {PositiveId}", paymentsToUpdate.Count, targetCourseIdNegative, evt.ClassId);
            }
            else
            {
                _logger.LogWarning("No waitlist payments found to update for CourseId: {CourseId} and UserIds: {UserIds}", evt.CourseId, string.Join(", ", evt.StudentUserIds));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing ClassCreatedFromQueueEvent in PaymentService for CourseId: {CourseId}", evt.CourseId);
            throw;
        }
    }
}
