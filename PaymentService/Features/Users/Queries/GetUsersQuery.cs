using MediatR;
using PaymentService.DTOs;

namespace PaymentService.Features.Users.Queries;

public record GetUsersQuery(
    string? Search,
    string? Role,
    bool? IsActive,
    int Page = 1,
    int PageSize = 10
) : IRequest<PagedResult<UserDto>>;
