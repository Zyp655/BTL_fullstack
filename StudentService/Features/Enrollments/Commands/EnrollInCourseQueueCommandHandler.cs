using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Models;
using StudentService.Services;
using StudentService.Repositories;
using MassTransit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudentService.Features.Enrollments.Commands;

public class EnrollInCourseQueueCommandHandler : IRequestHandler<EnrollInCourseQueueCommand, bool>
{
    private readonly StudentDbContext _context;
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseServiceClient _courseServiceClient;
    private readonly IPublishEndpoint _publishEndpoint;

    public EnrollInCourseQueueCommandHandler(
        StudentDbContext context,
        IStudentRepository studentRepository,
        ICourseServiceClient courseServiceClient,
        IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _studentRepository = studentRepository;
        _courseServiceClient = courseServiceClient;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<bool> Handle(EnrollInCourseQueueCommand request, CancellationToken cancellationToken)
    {
        // 1. Verify Student exists
        var student = await _studentRepository.GetStudentByIdAsync(request.StudentId);
        if (student == null)
            throw new KeyNotFoundException("Không tìm thấy hồ sơ học viên");

        // 2. Verify Course exists in CourseService
        var courseInfo = await _courseServiceClient.GetCourseInfo(request.CourseId);
        if (courseInfo == null)
            throw new KeyNotFoundException("Không tìm thấy thông tin khóa học");

        // 3. Check if already queued
        var alreadyQueued = await _context.CourseQueues
            .AnyAsync(q => q.StudentId == request.StudentId && q.CourseId == request.CourseId, cancellationToken);
        var alreadyHasPendingPayment = await _context.Enrollments
            .AnyAsync(e => e.StudentId == request.StudentId && e.ClassId == -request.CourseId, cancellationToken);
        if (alreadyQueued || alreadyHasPendingPayment)
            throw new ArgumentException("Học viên đã đăng ký hàng chờ cho khóa học này rồi (đang chờ thanh toán hoặc đã ở trong hàng)");

        // 4. Enroll in temporary Enrollment with ClassId = -CourseId and Status = "PendingPayment"
        var enrollment = new Enrollment
        {
            StudentId = request.StudentId,
            ClassId = -request.CourseId,
            Status = "PendingPayment",
            EnrolledAt = DateTime.UtcNow
        };

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync(cancellationToken);

        // 5. Publish StudentEnrolledEvent to RabbitMQ to generate invoice
        await _publishEndpoint.Publish<Contracts.StudentEnrolledEvent>(new Contracts.StudentEnrolledEvent
        {
            StudentId = enrollment.StudentId,
            UserId = student.UserId,
            ClassId = enrollment.ClassId,
            EnrolledAt = enrollment.EnrolledAt
        }, cancellationToken);

        return true;
    }
}
