using MediatR;
using PaymentService.DTOs;

namespace PaymentService.Features.Users.Commands;

public record LoginCommand(
    string Username,
    string Password
) : IRequest<LoginResponseDto?>;
