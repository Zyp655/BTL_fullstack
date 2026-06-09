using MediatR;
using Microsoft.EntityFrameworkCore;
using CourseService.Data;
using CourseService.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace CourseService.Features.Classes.Queries;

public class GetCourseAnalyticsQueryHandler : IRequestHandler<GetCourseAnalyticsQuery, CourseAnalyticsDto>
{
    private readonly CourseDbContext _context;

    public GetCourseAnalyticsQueryHandler(CourseDbContext context)
    {
        _context = context;
    }

    public async Task<CourseAnalyticsDto> Handle(GetCourseAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var classes = await _context.Classes
            .Include(c => c.Course)
            .ToListAsync(cancellationToken);

        // Class fill rates
        var classFillRates = classes.Select(c => new ClassFillRateDto
        {
            ClassId = c.ClassId,
            ClassName = c.ClassName,
            CourseName = c.Course?.CourseName ?? "Khóa học khác",
            CurrentStudents = c.CurrentStudents,
            MaxStudents = c.MaxStudents,
            FillRate = c.MaxStudents > 0 ? Math.Round((double)c.CurrentStudents / c.MaxStudents, 3) : 0
        }).ToList();

        // Average fill rate
        double averageFillRate = 0;
        if (classes.Any())
        {
            var totalMax = classes.Sum(c => c.MaxStudents);
            var totalCurrent = classes.Sum(c => c.CurrentStudents);
            averageFillRate = totalMax > 0 ? Math.Round((double)totalCurrent / totalMax, 3) : 0;
        }

        // Teacher workloads
        var teacherWorkloads = classes
            .Where(c => c.TeacherId.HasValue)
            .GroupBy(c => new { c.TeacherId, c.TeacherName })
            .Select(g => new TeacherWorkloadDto
            {
                TeacherId = g.Key.TeacherId!.Value,
                TeacherName = g.Key.TeacherName ?? "Giáo viên",
                ClassCount = g.Count(),
                TotalSessions = g.Sum(c => c.Course?.TotalSessions ?? 0)
            }).ToList();

        return new CourseAnalyticsDto
        {
            AverageClassFillRate = averageFillRate,
            ClassFillRates = classFillRates,
            TeacherWorkloads = teacherWorkloads
        };
    }
}
