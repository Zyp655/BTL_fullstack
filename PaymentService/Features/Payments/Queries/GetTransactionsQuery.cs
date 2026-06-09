using MediatR;
using PaymentService.DTOs;

namespace PaymentService.Features.Payments.Queries;

public record GetTransactionsQuery(int PaymentId) : IRequest<List<TransactionDto>>;
