using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Enrollments.Queries;

public record GetEnrollmentsQuery(
    int? ClassId,
    int? StudentId,
    string? Status,
    int Page = 1,
    int PageSize = 10
) : IRequest<PagedResult<EnrollmentDto>>;
