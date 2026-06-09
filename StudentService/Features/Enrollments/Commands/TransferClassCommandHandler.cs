using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.DTOs;
using StudentService.Models;
using StudentService.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudentService.Features.Enrollments.Commands;

public class TransferClassCommandHandler : IRequestHandler<TransferClassCommand, EnrollmentDto>
{
    private readonly StudentDbContext _context;
    private readonly ICourseServiceClient _courseServiceClient;
    private readonly IPublishEndpoint _publishEndpoint;

    public TransferClassCommandHandler(
        StudentDbContext context,
        ICourseServiceClient courseServiceClient,
        IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _courseServiceClient = courseServiceClient;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<EnrollmentDto> Handle(TransferClassCommand request, CancellationToken cancellationToken)
    {
        // 1. Verify Student exists
        var student = await _context.Students.FindAsync(new object[] { request.StudentId }, cancellationToken);
        if (student == null)
            throw new KeyNotFoundException("Không tìm thấy học viên");

        // 2. Fetch active enrollment in FromClassId
        var oldEnrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == request.StudentId && e.ClassId == request.FromClassId && e.Status == "DangHoc", cancellationToken);
        
        if (oldEnrollment == null)
            throw new KeyNotFoundException("Không tìm thấy bản ghi đăng ký học hoạt động ở lớp hiện tại.");

        // 3. Fetch Class Info from CourseService for both classes
        var oldClassInfo = await _courseServiceClient.GetClassInfo(request.FromClassId);
        if (oldClassInfo == null)
            throw new KeyNotFoundException("Không tìm thấy thông tin lớp học hiện tại");

        var targetClassInfo = await _courseServiceClient.GetClassInfo(request.ToClassId);
        if (targetClassInfo == null)
            throw new KeyNotFoundException("Không tìm thấy thông tin lớp học mới");

        // 4. Validate they belong to the same course
        if (oldClassInfo.CourseId != targetClassInfo.CourseId)
            throw new ArgumentException("Lớp học mới phải thuộc cùng một khóa học với lớp học hiện tại.");

        // 5. Validate capacity of target class
        if (targetClassInfo.CurrentStudents >= targetClassInfo.MaxStudents)
            throw new ArgumentException($"Lớp học mới '{targetClassInfo.ClassName}' đã đạt sĩ số tối đa ({targetClassInfo.MaxStudents} học viên).");

        // 6. Check if student is already enrolled in target class (any status)
        var existingTargetEnrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == request.StudentId && e.ClassId == request.ToClassId, cancellationToken);

        Enrollment activeEnrollment;
        if (existingTargetEnrollment != null)
        {
            if (existingTargetEnrollment.Status == "DangHoc" || existingTargetEnrollment.Status == "Active")
                throw new ArgumentException("Học viên đã đăng ký tham gia lớp học mới rồi.");

            // Reactivate/update existing enrollment
            existingTargetEnrollment.Status = "DangHoc";
            existingTargetEnrollment.EnrolledAt = DateTime.UtcNow;
            _context.Enrollments.Update(existingTargetEnrollment);
            activeEnrollment = existingTargetEnrollment;
        }
        else
        {
            // Create new enrollment
            var newEnrollment = new Enrollment
            {
                StudentId = request.StudentId,
                ClassId = request.ToClassId,
                Status = "DangHoc",
                EnrolledAt = DateTime.UtcNow
            };
            _context.Enrollments.Add(newEnrollment);
            activeEnrollment = newEnrollment;
        }

        // 7. Deactivate old enrollment
        oldEnrollment.Status = "HuyBo"; // Mark old as cancelled/transferred
        _context.Enrollments.Update(oldEnrollment);

        // 9. Publish event to RabbitMQ so PaymentService updates the invoice to the new class
        var resolutionItems = new List<Contracts.StudentResolutionItem>
        {
            new Contracts.StudentResolutionItem
            {
                StudentUserId = student.UserId,
                Action = "ChuyenLop",
                NewClassId = request.ToClassId
            }
        };

        await _publishEndpoint.Publish<Contracts.ResolveCancelledClassEvent>(new Contracts.ResolveCancelledClassEvent
        {
            ClassId = request.FromClassId,
            Resolutions = resolutionItems
        }, cancellationToken);

        // 10. Save changes
        await _context.SaveChangesAsync(cancellationToken);

        // 11. Map and return
        var result = EnrollmentMapper.MapToDto(activeEnrollment);
        result.StudentName = student.FullName;
        result.ClassName = targetClassInfo.ClassName;
        result.CourseName = targetClassInfo.CourseName;
        return result;
    }
}
