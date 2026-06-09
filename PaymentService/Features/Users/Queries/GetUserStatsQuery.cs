using MediatR;
using PaymentService.DTOs;
using PaymentService.Repositories;

namespace PaymentService.Features.Users.Queries;

public record GetUserStatsQuery : IRequest<UserStatsDto>;

public class GetUserStatsQueryHandler : IRequestHandler<GetUserStatsQuery, UserStatsDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserStatsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserStatsDto> Handle(GetUserStatsQuery request, CancellationToken cancellationToken)
    {
        var total = await _userRepository.GetUsersCountAsync(null, null, null);
        var admin = await _userRepository.GetUsersCountAsync(null, "Admin", null);
        var teacher = await _userRepository.GetUsersCountAsync(null, "GiaoVien", null);
        var student = await _userRepository.GetUsersCountAsync(null, "HocVien", null);

        return new UserStatsDto
        {
            TotalCount = total,
            AdminCount = admin,
            TeacherCount = teacher,
            StudentCount = student
        };
    }
}
