using MediatR;

namespace PaymentService.Features.Users.Commands;

public record ChangePasswordCommand(
    int Id,
    string CurrentPassword,
    string NewPassword
) : IRequest<bool>;
