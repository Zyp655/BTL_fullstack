using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Courses.Queries;

public record GetCoursesQuery(
    string? Search,
    string? Category,
    string? Level,
    bool? IsActive,
    int Page = 1,
    int PageSize = 10
) : IRequest<PagedResult<CourseDto>>;
