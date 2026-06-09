using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Models;
using StudentService.Services;
using StudentService.Repositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudentService.Features.Enrollments.Commands;

public class LaunchClassFromQueueCommandHandler : IRequestHandler<LaunchClassFromQueueCommand, bool>
{
    private readonly StudentDbContext _context;
    private readonly ICourseServiceClient _courseServiceClient;
    private readonly IPublishEndpoint _publishEndpoint;

    public LaunchClassFromQueueCommandHandler(
        StudentDbContext context,
        ICourseServiceClient courseServiceClient,
        IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _courseServiceClient = courseServiceClient;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<bool> Handle(LaunchClassFromQueueCommand request, CancellationToken cancellationToken)
    {
        // 1. Fetch Course Info
        var courseInfo = await _courseServiceClient.GetCourseInfo(request.CourseId);
        if (courseInfo == null)
            throw new KeyNotFoundException("Không tìm thấy thông tin khóa học");

        // 2. Fetch Students and check queue
        var students = await _context.Students
            .Where(s => request.StudentIds.Contains(s.StudentId))
            .ToListAsync(cancellationToken);

        var queueEntries = await _context.CourseQueues
            .Where(q => q.CourseId == request.CourseId && request.StudentIds.Contains(q.StudentId))
            .ToListAsync(cancellationToken);

        if (queueEntries.Count == 0)
            throw new InvalidOperationException("Không tìm thấy học viên nào trong hàng chờ của khóa học này");

        var studentUserIds = new List<int>();

        foreach (var studentId in request.StudentIds)
        {
            var student = students.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null) continue;

            studentUserIds.Add(student.UserId);

            // Remove from queue if exists
            var queueEntry = queueEntries.FirstOrDefault(q => q.StudentId == studentId);
            if (queueEntry != null)
            {
                _context.CourseQueues.Remove(queueEntry);
            }

            // Create enrollment
            var alreadyEnrolled = await _context.Enrollments
                .AnyAsync(e => e.StudentId == studentId && e.ClassId == request.ClassId, cancellationToken);

            if (!alreadyEnrolled)
            {
                var enrollment = new Enrollment
                {
                    StudentId = studentId,
                    ClassId = request.ClassId,
                    Status = "DangHoc",
                    EnrolledAt = DateTime.UtcNow
                };
                _context.Enrollments.Add(enrollment);
            }
        }

        // 3. Save DB Changes
        await _context.SaveChangesAsync(cancellationToken);

        // 5. Publish ClassCreatedFromQueueEvent to update PaymentService invoices
        await _publishEndpoint.Publish<Contracts.ClassCreatedFromQueueEvent>(new Contracts.ClassCreatedFromQueueEvent
        {
            ClassId = request.ClassId,
            CourseId = request.CourseId,
            CourseName = courseInfo.CourseName,
            StudentIds = request.StudentIds,
            StudentUserIds = studentUserIds
        }, cancellationToken);

        return true;
    }
}
