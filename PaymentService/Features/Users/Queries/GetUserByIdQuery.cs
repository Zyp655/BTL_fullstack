using MediatR;
using PaymentService.DTOs;

namespace PaymentService.Features.Users.Queries;

public record GetUserByIdQuery(int Id) : IRequest<UserDto?>;
