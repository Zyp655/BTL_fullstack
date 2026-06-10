using MediatR;
using PaymentService.DTOs;

namespace PaymentService.Features.Users.Commands;

public record RegisterCommand(
    string Username,
    string Password,
    string FullName,
    string? Email,
    string? Phone,
    string Role,
    string? Specialization = null,
    string? Degree = null
) : IRequest<UserDto>;
