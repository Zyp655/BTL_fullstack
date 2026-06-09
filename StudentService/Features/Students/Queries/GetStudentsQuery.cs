using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Students.Queries;

public record GetStudentsQuery(
    string? Search,
    string? Gender,
    int Page = 1,
    int PageSize = 10
) : IRequest<PagedResult<StudentDto>>;
