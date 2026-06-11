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

public class ResolveCancelledClassCommandHandler : IRequestHandler<ResolveCancelledClassCommand, bool>
{
    private readonly StudentDbContext _context;
    private readonly ICourseServiceClient _courseServiceClient;
    private readonly IPublishEndpoint _publishEndpoint;

    public ResolveCancelledClassCommandHandler(
        StudentDbContext context,
        ICourseServiceClient courseServiceClient,
        IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _courseServiceClient = courseServiceClient;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<bool> Handle(ResolveCancelledClassCommand request, CancellationToken cancellationToken)
    {
        // 1. Fetch Class Info from CourseService to get course fee
        var classInfo = await _courseServiceClient.GetClassInfo(request.ClassId);
        if (classInfo == null)
            throw new KeyNotFoundException("Không tìm thấy thông tin lớp học");

        var courseInfo = await _courseServiceClient.GetCourseInfo(classInfo.CourseId);
        if (courseInfo == null)
            throw new KeyNotFoundException("Không tìm thấy thông tin khóa học");

        // 2. Fetch all enrollments for this class
        var enrollments = await _context.Enrollments
            .Include(e => e.Student)
            .Where(e => e.ClassId == request.ClassId)
            .ToListAsync(cancellationToken);

        var resolutionItems = new List<Contracts.StudentResolutionItem>();

        foreach (var res in request.Resolutions)
        {
            var enrollment = enrollments.FirstOrDefault(e => e.StudentId == res.StudentId);
            if (enrollment == null) continue;

            // Resolve student user ID for the payment integration event
            int userId = enrollment.Student?.UserId ?? 0;
            if (userId == 0)
            {
                // Fallback: load student from DB
                var student = await _context.Students.FindAsync(new object[] { res.StudentId }, cancellationToken);
                userId = student?.UserId ?? 0;
            }

            if (res.Action == "BaoLuu")
            {
                // Create Credit Balance for Student
                var credit = new StudentCredit
                {
                    StudentId = res.StudentId,
                    Amount = courseInfo.Fee,
                    SourceClassId = request.ClassId,
                    Status = "Available",
                    CreatedAt = DateTime.UtcNow
                };
                _context.StudentCredits.Add(credit);

                // Set old enrollment status to Cancelled (HuyBo)
                enrollment.Status = "HuyBo";
                _context.Enrollments.Update(enrollment);

                resolutionItems.Add(new Contracts.StudentResolutionItem
                {
                    StudentUserId = userId,
                    Action = "BaoLuu",
                    NewClassId = null
                });
            }
            else if (res.Action == "ChuyenLop")
            {
                if (!res.NewClassId.HasValue)
                    throw new ArgumentException($"Chuyển lớp yêu cầu ID lớp mới cho học viên #{res.StudentId}");

                var newClassInfo = await _courseServiceClient.GetClassInfo(res.NewClassId.Value);
                if (newClassInfo == null)
                    throw new KeyNotFoundException($"Không tìm thấy thông tin lớp học mới #{res.NewClassId.Value}");

                // Check scheduling conflicts (excluding FromClassId which is request.ClassId)
                var activeClassIds = await _context.Enrollments
                    .Where(e => e.StudentId == res.StudentId && e.Status == "DangHoc" && e.ClassId != request.ClassId)
                    .Select(e => e.ClassId)
                    .ToListAsync(cancellationToken);

                await StudentConflictHelper.CheckStudentConflicts(_courseServiceClient, res.NewClassId.Value, newClassInfo, activeClassIds);

                // Deactivate old enrollment
                enrollment.Status = "HuyBo";
                _context.Enrollments.Update(enrollment);

                // Check if already enrolled in the new class
                var existingTargetEnrollment = await _context.Enrollments
                    .FirstOrDefaultAsync(e => e.StudentId == res.StudentId && e.ClassId == res.NewClassId.Value, cancellationToken);

                if (existingTargetEnrollment != null)
                {
                    existingTargetEnrollment.Status = "DangHoc";
                    existingTargetEnrollment.EnrolledAt = DateTime.UtcNow;
                    _context.Enrollments.Update(existingTargetEnrollment);
                }
                else
                {
                    var newEnrollment = new Enrollment
                    {
                        StudentId = res.StudentId,
                        ClassId = res.NewClassId.Value,
                        Status = "DangHoc",
                        EnrolledAt = DateTime.UtcNow
                    };
                    _context.Enrollments.Add(newEnrollment);
                }

                // Decrement old class student count and increment new class student count (handled asynchronously via ResolveCancelledClassEvent consumer)


                resolutionItems.Add(new Contracts.StudentResolutionItem
                {
                    StudentUserId = userId,
                    Action = "ChuyenLop",
                    NewClassId = res.NewClassId.Value
                });
            }
            else if (res.Action == "HoanTien")
            {
                // Set old enrollment status to Cancelled (HuyBo)
                enrollment.Status = "HuyBo";
                _context.Enrollments.Update(enrollment);

                resolutionItems.Add(new Contracts.StudentResolutionItem
                {
                    StudentUserId = userId,
                    Action = "HoanTien",
                    NewClassId = null
                });
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        // 3. Publish ResolveCancelledClassEvent to RabbitMQ to update PaymentService invoices
        if (resolutionItems.Count > 0)
        {
            await _publishEndpoint.Publish<Contracts.ResolveCancelledClassEvent>(new Contracts.ResolveCancelledClassEvent
            {
                ClassId = request.ClassId,
                Resolutions = resolutionItems
            }, cancellationToken);
        }

        return true;
    }
}
