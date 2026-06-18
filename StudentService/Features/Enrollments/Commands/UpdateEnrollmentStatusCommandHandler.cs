using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;
using StudentService.Services;
using StudentService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudentService.Features.Enrollments.Commands;

public class UpdateEnrollmentStatusCommandHandler : IRequestHandler<UpdateEnrollmentStatusCommand, EnrollmentDto?>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseServiceClient _courseServiceClient;
    private readonly StudentDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateEnrollmentStatusCommandHandler(
        IEnrollmentRepository enrollmentRepository, 
        ICourseServiceClient courseServiceClient,
        StudentDbContext context,
        IPublishEndpoint publishEndpoint)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseServiceClient = courseServiceClient;
        _context = context;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<EnrollmentDto?> Handle(UpdateEnrollmentStatusCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(request.Id);
        if (enrollment == null) return null;

        var oldStatus = enrollment.Status;
        enrollment.Status = request.Status;
        if (request.Status == "HoanThanh")
            enrollment.CompletedAt = DateTime.UtcNow;

        _enrollmentRepository.UpdateEnrollment(enrollment);
        await _enrollmentRepository.SaveChangesAsync();

        // Check if student attended <= 1 session and is being cancelled (HuyBo or Cancelled)
        if ((request.Status == "HuyBo" || request.Status == "Cancelled") && oldStatus != "HuyBo" && oldStatus != "Cancelled")
        {
            var student = await _context.Students.FindAsync(new object[] { enrollment.StudentId }, cancellationToken);
            if (student != null)
            {
                int attendedCount = await _context.Attendances
                    .CountAsync(a => a.EnrollmentId == enrollment.EnrollmentId && (a.Status == "CoMat" || a.Status == "DiTre"), cancellationToken);

                if (attendedCount <= 1)
                {
                    await _publishEndpoint.Publish<Contracts.SingleSessionRefundRequestEvent>(new Contracts.SingleSessionRefundRequestEvent
                    {
                        StudentUserId = student.UserId,
                        ClassId = enrollment.ClassId
                    }, cancellationToken);
                }
            }
        }

        var result = EnrollmentMapper.MapToDto(enrollment);
        var classInfo = await _courseServiceClient.GetClassInfo(enrollment.ClassId);
        if (classInfo != null)
        {
            result.ClassName = classInfo.ClassName;
            result.CourseName = classInfo.CourseName;
            result.Room = classInfo.Room;
            result.StartDate = classInfo.StartDate;
            result.EndDate = classInfo.EndDate;
        }
        return result;
    }
}
