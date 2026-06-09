using System;
using System.Threading.Tasks;
using MassTransit;
using Contracts;
using StudentService.Data;
using StudentService.Models;
using StudentService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.Generic;

namespace StudentService.Consumers;

public class PaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
{
    private readonly StudentDbContext _context;
    private readonly ICourseServiceClient _courseServiceClient;
    private readonly ILogger<PaymentCompletedConsumer> _logger;

    public PaymentCompletedConsumer(StudentDbContext context, ICourseServiceClient courseServiceClient, ILogger<PaymentCompletedConsumer> logger)
    {
        _context = context;
        _courseServiceClient = courseServiceClient;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        var evt = context.Message;
        _logger.LogInformation("Processing PaymentCompletedEvent for StudentUserId: {StudentUserId}, ClassId: {ClassId}", evt.StudentUserId, evt.ClassId);

        try
        {
            // 1. Fetch Student using StudentUserId (UserId in Student table)
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == evt.StudentUserId);

            if (student == null)
            {
                _logger.LogWarning("Student not found for UserId: {StudentUserId}", evt.StudentUserId);
                return;
            }

            // Check if waitlist payment (ClassId < 0)
            if (evt.ClassId < 0)
            {
                var courseId = -evt.ClassId;

                // 2. Fetch Enrollment for StudentId and ClassId
                var enrollment = await _context.Enrollments
                    .FirstOrDefaultAsync(e => e.StudentId == student.StudentId && e.ClassId == evt.ClassId);

                if (enrollment != null)
                {
                    _context.Enrollments.Remove(enrollment);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Deleted temporary waitlist enrollment ID: {EnrollmentId} for StudentId: {StudentId}, CourseId: {CourseId}", enrollment.EnrollmentId, student.StudentId, courseId);
                }

                // 3. Add to CourseQueues
                var alreadyQueued = await _context.CourseQueues
                    .AnyAsync(q => q.StudentId == student.StudentId && q.CourseId == courseId);

                if (!alreadyQueued)
                {
                    var queueRecord = new CourseQueue
                    {
                        StudentId = student.StudentId,
                        CourseId = courseId,
                        QueuedAt = DateTime.UtcNow
                    };
                    _context.CourseQueues.Add(queueRecord);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("StudentId: {StudentId} successfully added to CourseQueues for CourseId: {CourseId}", student.StudentId, courseId);
                }

                // 4. Check queue size
                var queuedList = await _context.CourseQueues
                    .Where(q => q.CourseId == courseId)
                    .OrderBy(q => q.QueuedAt)
                    .ToListAsync();

                if (queuedList.Count >= 5)
                {
                    var studentsToClass = queuedList.Take(5).ToList();
                    var studentIds = studentsToClass.Select(s => s.StudentId).ToList();

                    var studentUserIds = await _context.Students
                        .Where(s => studentIds.Contains(s.StudentId))
                        .Select(s => s.UserId)
                        .ToListAsync();

                    var courseInfo = await _courseServiceClient.GetCourseInfo(courseId);
                    var courseName = courseInfo?.CourseName ?? $"Khóa học #{courseId}";

                    // Publish Event to RabbitMQ for CourseService to process class auto-creation
                    await context.Publish<CourseQueueFullEvent>(new CourseQueueFullEvent
                    {
                        CourseId = courseId,
                        CourseName = courseName,
                        StudentIds = studentIds,
                        StudentUserIds = studentUserIds
                    });

                    _logger.LogInformation("Published CourseQueueFullEvent for CourseId {CourseId} with 5 students.", courseId);

                    // Remove these 5 from the queue
                    _context.CourseQueues.RemoveRange(studentsToClass);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                // Regular class enrollment payment completion
                // 2. Fetch Enrollment for StudentId and ClassId
                var enrollment = await _context.Enrollments
                    .FirstOrDefaultAsync(e => e.StudentId == student.StudentId && e.ClassId == evt.ClassId);

                if (enrollment == null)
                {
                    _logger.LogWarning("Enrollment not found for StudentId: {StudentId}, ClassId: {ClassId}", student.StudentId, evt.ClassId);
                    return;
                }

                // 3. Update Status to "DangHoc"
                if (enrollment.Status != "DangHoc")
                {
                    enrollment.Status = "DangHoc";
                    _context.Enrollments.Update(enrollment);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Successfully activated enrollment ID: {EnrollmentId} (Status set to DangHoc) for ClassId: {ClassId}", enrollment.EnrollmentId, evt.ClassId);
                }
                else
                {
                    _logger.LogInformation("Enrollment ID: {EnrollmentId} was already active (DangHoc)", enrollment.EnrollmentId);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing PaymentCompletedEvent for StudentUserId: {StudentUserId}, ClassId: {ClassId}", evt.StudentUserId, evt.ClassId);
            throw;
        }
    }
}
