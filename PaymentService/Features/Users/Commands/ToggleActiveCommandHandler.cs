using MediatR;
using PaymentService.Repositories;

namespace PaymentService.Features.Users.Commands;

public class ToggleActiveCommandHandler : IRequestHandler<ToggleActiveCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public ToggleActiveCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(ToggleActiveCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.Id);
        if (user == null) return false;

        user.IsActive = !user.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        _userRepository.UpdateUser(user);
        await _userRepository.SaveChangesAsync();

        return true;
    }
}
