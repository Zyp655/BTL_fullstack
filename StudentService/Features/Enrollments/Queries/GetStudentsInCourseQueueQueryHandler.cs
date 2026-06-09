using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudentService.Features.Enrollments.Queries;

public class GetStudentsInCourseQueueQueryHandler : IRequestHandler<GetStudentsInCourseQueueQuery, List<StudentDto>>
{
    private readonly StudentDbContext _context;

    public GetStudentsInCourseQueueQueryHandler(StudentDbContext context)
    {
        _context = context;
    }

    public async Task<List<StudentDto>> Handle(GetStudentsInCourseQueueQuery request, CancellationToken cancellationToken)
    {
        var students = await _context.CourseQueues
            .Where(q => q.CourseId == request.CourseId)
            .Include(q => q.Student)
            .Select(q => q.Student)
            .Where(s => s != null)
            .Select(s => new StudentDto
            {
                StudentId = s!.StudentId,
                UserId = s.UserId,
                FullName = s.FullName,
                DateOfBirth = s.DateOfBirth,
                Gender = s.Gender,
                Phone = s.Phone,
                Email = s.Email,
                Address = s.Address,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        return students;
    }
}
