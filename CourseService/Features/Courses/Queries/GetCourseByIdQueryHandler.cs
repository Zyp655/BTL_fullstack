using MediatR;
using CourseService.DTOs;
using CourseService.Repositories;
using CourseService.Common.Exceptions;

namespace CourseService.Features.Courses.Queries;

public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseDto>
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseByIdQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<CourseDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetCourseByIdAsync(request.Id);
        if (course == null)
            throw new NotFoundException("Khóa học", request.Id);

        return new CourseDto
        {
            CourseId = course.CourseId,
            CourseName = course.CourseName,
            Description = course.Description,
            Level = course.Level,
            Category = course.Category,
            Fee = course.Fee,
            TotalSessions = course.TotalSessions,
            IsActive = course.IsActive,
            CreatedAt = course.CreatedAt,
            UpdatedAt = course.UpdatedAt,
            ClassCount = course.Classes?.Count ?? 0
        };
    }
}
