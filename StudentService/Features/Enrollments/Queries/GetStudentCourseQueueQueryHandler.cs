using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudentService.Features.Enrollments.Queries;

public class GetStudentCourseQueueQueryHandler : IRequestHandler<GetStudentCourseQueueQuery, List<int>>
{
    private readonly StudentDbContext _context;

    public GetStudentCourseQueueQueryHandler(StudentDbContext context)
    {
        _context = context;
    }

    public async Task<List<int>> Handle(GetStudentCourseQueueQuery request, CancellationToken cancellationToken)
    {
        var queuedCourseIds = await _context.CourseQueues
            .Where(q => q.StudentId == request.StudentId)
            .Select(q => q.CourseId)
            .ToListAsync(cancellationToken);

        var pendingCourseIds = await _context.Enrollments
            .Where(e => e.StudentId == request.StudentId && e.ClassId < 0)
            .Select(e => -e.ClassId)
            .ToListAsync(cancellationToken);

        return queuedCourseIds.Concat(pendingCourseIds).Distinct().ToList();
    }
}
