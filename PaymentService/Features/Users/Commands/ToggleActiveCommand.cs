using MediatR;

namespace PaymentService.Features.Users.Commands;

public record ToggleActiveCommand(int Id) : IRequest<bool>;
