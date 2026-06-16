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
            .SelectMany(c => {
                var list = new List<(int Id, string Name, int TotalSessions)>();
                if (c.TeacherId.HasValue)
                {
                    list.Add((c.TeacherId.Value, c.TeacherName ?? "Giáo viên", c.Course?.TotalSessions ?? 0));
                }
                if (c.TeacherId2.HasValue)
                {
                    list.Add((c.TeacherId2.Value, c.TeacherName2 ?? "Giáo viên phụ", c.Course?.TotalSessions ?? 0));
                }
                return list;
            })
            .GroupBy(x => new { x.Id, x.Name })
            .Select(g => new TeacherWorkloadDto
            {
                TeacherId = g.Key.Id,
                TeacherName = g.Key.Name,
                ClassCount = g.Count(),
                TotalSessions = g.Sum(x => x.TotalSessions)
            }).ToList();

        return new CourseAnalyticsDto
        {
            AverageClassFillRate = averageFillRate,
            ClassFillRates = classFillRates,
            TeacherWorkloads = teacherWorkloads
        };
    }
}
