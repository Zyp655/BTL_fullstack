using MediatR;
using PaymentService.DTOs;
using PaymentService.Repositories;
using PaymentService.Services;

namespace PaymentService.Features.Users.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return null;

        if (!user.IsActive)
            throw new UnauthorizedAccessException("Tài khoản đã bị khóa");

        var token = _tokenService.GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token,
            User = UserMapper.MapToDto(user)
        };
    }
}
