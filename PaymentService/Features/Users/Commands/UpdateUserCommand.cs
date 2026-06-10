using MediatR;
using PaymentService.DTOs;

namespace PaymentService.Features.Users.Commands;

public record UpdateUserCommand(
    int Id,
    string FullName,
    string? Email,
    string? Phone,
    string Role,
    string? Specialization = null,
    string? Degree = null
) : IRequest<UserDto?>;
