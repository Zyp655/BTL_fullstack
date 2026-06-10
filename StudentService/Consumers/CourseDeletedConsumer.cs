using System;
using System.Threading.Tasks;
using MassTransit;
using Contracts;
using StudentService.Data;
using StudentService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace StudentService.Consumers;

public class CourseDeletedConsumer : IConsumer<CourseDeletedEvent>
{
    private readonly StudentDbContext _context;
    private readonly ILogger<CourseDeletedConsumer> _logger;

    public CourseDeletedConsumer(StudentDbContext context, ILogger<CourseDeletedConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CourseDeletedEvent> context)
    {
        var evt = context.Message;
        _logger.LogInformation("Processing CourseDeletedEvent for CourseId: {CourseId}", evt.CourseId);

        try
        {
            if (evt.ClassIds == null || evt.ClassIds.Count == 0)
            {
                _logger.LogInformation("No class IDs provided for CourseId: {CourseId}. Nothing to process.", evt.CourseId);
                return;
            }

            // Get all active or non-cancelled enrollments in these classes
            var enrollments = await _context.Enrollments
                .Where(e => evt.ClassIds.Contains(e.ClassId) && e.Status != "HuyBo")
                .ToListAsync();

            _logger.LogInformation("Found {Count} active enrollments to process for CourseId: {CourseId}", enrollments.Count, evt.CourseId);

            foreach (var enrollment in enrollments)
            {
                // 1. Cancel the current enrollment
                enrollment.Status = "HuyBo";
                _context.Enrollments.Update(enrollment);

                // 2. Add back to the course waitlist queue
                var alreadyQueued = await _context.CourseQueues
                    .AnyAsync(q => q.StudentId == enrollment.StudentId && q.CourseId == evt.CourseId);

                if (!alreadyQueued)
                {
                    var queueRecord = new CourseQueue
                    {
                        StudentId = enrollment.StudentId,
                        CourseId = evt.CourseId,
                        QueuedAt = DateTime.UtcNow
                    };
                    _context.CourseQueues.Add(queueRecord);
                    _logger.LogInformation("Returned StudentId: {StudentId} to CourseQueue for CourseId: {CourseId}", enrollment.StudentId, evt.CourseId);
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully processed CourseDeletedEvent for CourseId: {CourseId}", evt.CourseId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing CourseDeletedEvent for CourseId: {CourseId}", evt.CourseId);
            throw;
        }
    }
}
