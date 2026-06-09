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

public class ClassCreatedFromQueueConsumer : IConsumer<ClassCreatedFromQueueEvent>
{
    private readonly StudentDbContext _context;
    private readonly ILogger<ClassCreatedFromQueueConsumer> _logger;

    public ClassCreatedFromQueueConsumer(StudentDbContext context, ILogger<ClassCreatedFromQueueConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ClassCreatedFromQueueEvent> context)
    {
        var evt = context.Message;
        _logger.LogInformation("Processing ClassCreatedFromQueueEvent for ClassId: {ClassId}, CourseId: {CourseId}", evt.ClassId, evt.CourseId);

        try
        {
            // Fetch all student user accounts in one query
            var students = await _context.Students
                .Where(s => evt.StudentIds.Contains(s.StudentId))
                .ToListAsync();

            foreach (var student in students)
            {
                // Verify if already enrolled in this class
                var exists = await _context.Enrollments
                    .AnyAsync(e => e.StudentId == student.StudentId && e.ClassId == evt.ClassId);

                if (exists) continue;

                // Create Enrollment
                var enrollment = new Enrollment
                {
                    StudentId = student.StudentId,
                    ClassId = evt.ClassId,
                    Status = "DangHoc",
                    EnrolledAt = DateTime.UtcNow
                };

                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully enrolled StudentId {StudentId} in ClassId {ClassId} with Status DangHoc", student.StudentId, evt.ClassId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error enrolling students from queue for ClassId: {ClassId}", evt.ClassId);
            throw;
        }
    }
}
