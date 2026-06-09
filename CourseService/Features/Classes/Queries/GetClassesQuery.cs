using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Classes.Queries;

public record GetClassesQuery(
    int? CourseId,
    int? TeacherId,
    string? Status,
    string? Search,
    int Page = 1,
    int PageSize = 10
) : IRequest<PagedResult<ClassDto>>;
