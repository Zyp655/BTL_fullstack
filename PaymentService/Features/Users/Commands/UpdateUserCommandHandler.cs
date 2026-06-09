using MediatR;
using PaymentService.DTOs;
using PaymentService.Repositories;

namespace PaymentService.Features.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.Id);
        if (user == null) return null;

        user.FullName = request.FullName;
        user.Email = request.Email;
        user.Phone = request.Phone;
        user.Role = request.Role;
        user.UpdatedAt = DateTime.UtcNow;

        _userRepository.UpdateUser(user);
        await _userRepository.SaveChangesAsync();

        return UserMapper.MapToDto(user);
    }
}
