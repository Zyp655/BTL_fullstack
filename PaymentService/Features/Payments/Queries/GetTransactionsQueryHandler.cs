using MediatR;
using PaymentService.DTOs;
using PaymentService.Repositories;

namespace PaymentService.Features.Payments.Queries;

public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionDto>>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetTransactionsQueryHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<List<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _paymentRepository.GetTransactionsByPaymentIdAsync(request.PaymentId);
        return transactions.Select(t => new TransactionDto
        {
            TransactionId = t.TransactionId,
            PaymentId = t.PaymentId,
            Amount = t.Amount,
            PaymentMethod = t.PaymentMethod,
            Note = t.Note,
            ReceivedByUserId = t.ReceivedByUserId,
            PaidAt = t.PaidAt
        }).ToList();
    }
}
