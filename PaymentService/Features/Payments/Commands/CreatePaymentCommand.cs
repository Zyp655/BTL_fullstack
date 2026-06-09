using MediatR;
using PaymentService.DTOs;

namespace PaymentService.Features.Payments.Commands;

public record CreatePaymentCommand(
    int StudentUserId,
    int ClassId,
    decimal TotalAmount,
    DateTime DueDate
) : IRequest<PaymentDto>;
