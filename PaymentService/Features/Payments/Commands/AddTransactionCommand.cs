using MediatR;
using PaymentService.DTOs;

namespace PaymentService.Features.Payments.Commands;

public record AddTransactionCommand(
    int PaymentId,
    decimal Amount,
    string PaymentMethod,
    string? Note,
    int ReceivedByUserId
) : IRequest<TransactionDto>;
