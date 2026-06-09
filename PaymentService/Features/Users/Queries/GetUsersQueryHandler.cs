using MediatR;
using PaymentService.DTOs;
using PaymentService.Repositories;

namespace PaymentService.Features.Users.Queries;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedResult<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PagedResult<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var items = await _userRepository.GetUsersAsync(request.Search, request.Role, request.IsActive, request.Page, request.PageSize);
        var totalCount = await _userRepository.GetUsersCountAsync(request.Search, request.Role, request.IsActive);

        return new PagedResult<UserDto>
        {
            Items = items.Select(UserMapper.MapToDto).ToList(),
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
