using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.DTOs;
using StudentService.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace StudentService.Features.Enrollments.Queries;

public class GetStudentAnalyticsQueryHandler : IRequestHandler<GetStudentAnalyticsQuery, StudentAnalyticsDto>
{
    private readonly StudentDbContext _context;
    private readonly ICourseServiceClient _courseServiceClient;

    public GetStudentAnalyticsQueryHandler(StudentDbContext context, ICourseServiceClient courseServiceClient)
    {
        _context = context;
        _courseServiceClient = courseServiceClient;
    }

    public async Task<StudentAnalyticsDto> Handle(GetStudentAnalyticsQuery request, CancellationToken cancellationToken)
    {
        // 1. Waitlists
        var queueGroups = await _context.CourseQueues
            .GroupBy(q => q.CourseId)
            .Select(g => new { CourseId = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);

        var waitlists = new List<WaitlistAnalyticsDto>();
        foreach (var group in queueGroups)
        {
            var courseInfo = await _courseServiceClient.GetCourseInfo(group.CourseId);
            waitlists.Add(new WaitlistAnalyticsDto
            {
                CourseId = group.CourseId,
                CourseName = courseInfo?.CourseName ?? $"Khóa học #{group.CourseId}",
                QueueCount = group.Count
            });
        }

        // 2. Credits stats
        var creditStats = await _context.StudentCredits
            .GroupBy(c => c.Status)
            .Select(g => new { Status = g.Key, Total = g.Sum(c => c.Amount) })
            .ToListAsync(cancellationToken);

        decimal totalAvailable = creditStats.FirstOrDefault(s => s.Status == "Available")?.Total ?? 0;
        decimal totalUsed = creditStats.FirstOrDefault(s => s.Status == "Used")?.Total ?? 0;
        decimal totalRefunded = creditStats.FirstOrDefault(s => s.Status == "Refunded")?.Total ?? 0;

        // 3. Academic warnings (attendance < 80% or absent >= 2)
        var enrollments = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Attendances)
            .Where(e => e.ClassId > 0 && e.Attendances.Any())
            .ToListAsync(cancellationToken);

        var warnings = new List<AcademicWarningDto>();
        var classCache = new Dictionary<int, string>(); // classId -> className

        foreach (var e in enrollments)
        {
            var total = e.Attendances.Count;
            var present = e.Attendances.Count(a => a.Status == "CoMat");
            var absent = e.Attendances.Count(a => a.Status == "Vang");
            var late = e.Attendances.Count(a => a.Status == "DiTre");
            var excused = e.Attendances.Count(a => a.Status == "CoPhep");
            
            var attendanceRate = total > 0 ? Math.Min(100.0, Math.Round((double)(present + excused + late) / total * 100, 1)) : 100.0;

            if (attendanceRate < 80 || absent >= 2)
            {
                if (!classCache.TryGetValue(e.ClassId, out var className))
                {
                    var classInfo = await _courseServiceClient.GetClassInfo(e.ClassId);
                    className = classInfo?.ClassName ?? $"Lớp #{e.ClassId}";
                    classCache[e.ClassId] = className;
                }

                warnings.Add(new AcademicWarningDto
                {
                    StudentId = e.StudentId,
                    StudentName = e.Student?.FullName ?? "Học viên",
                    ClassId = e.ClassId,
                    ClassName = className,
                    TotalSessions = total,
                    AbsentCount = absent,
                    LateCount = late,
                    AttendanceRate = attendanceRate
                });
            }
        }

        return new StudentAnalyticsDto
        {
            Waitlists = waitlists.OrderByDescending(w => w.QueueCount).ToList(),
            TotalCreditsAvailable = totalAvailable,
            TotalCreditsUsed = totalUsed,
            TotalCreditsRefunded = totalRefunded,
            AcademicWarnings = warnings.OrderBy(w => w.AttendanceRate).ToList()
        };
    }
}
