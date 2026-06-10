using MediatR;
using PaymentService.DTOs;
using PaymentService.Models;
using PaymentService.Repositories;

namespace PaymentService.Features.Users.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByUsernameAsync(request.Username))
            throw new ArgumentException("Tên đăng nhập đã tồn tại");

        var user = new User
        {
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            FullName = request.FullName,
            Email = request.Email,
            Phone = request.Phone,
            Role = request.Role,
            Specialization = request.Specialization,
            Degree = request.Degree,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _userRepository.AddUserAsync(user);
        await _userRepository.SaveChangesAsync();

        return UserMapper.MapToDto(user);
    }
}
