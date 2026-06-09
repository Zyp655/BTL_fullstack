using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudentService.Features.Enrollments.Queries;

public class GetCourseQueueStatusQueryHandler : IRequestHandler<GetCourseQueueStatusQuery, List<CourseQueueStatusDto>>
{
    private readonly StudentDbContext _context;

    public GetCourseQueueStatusQueryHandler(StudentDbContext context)
    {
        _context = context;
    }

    public async Task<List<CourseQueueStatusDto>> Handle(GetCourseQueueStatusQuery request, CancellationToken cancellationToken)
    {
        var counts = await _context.CourseQueues
            .GroupBy(q => q.CourseId)
            .Select(g => new CourseQueueStatusDto
            {
                CourseId = g.Key,
                Count = g.Count()
            })
            .ToListAsync(cancellationToken);

        return counts;
    }
}
