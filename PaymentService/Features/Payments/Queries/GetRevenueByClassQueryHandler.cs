using MediatR;
using PaymentService.Repositories;

namespace PaymentService.Features.Payments.Queries;

public class GetRevenueByClassQueryHandler : IRequestHandler<GetRevenueByClassQuery, object>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetRevenueByClassQueryHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<object> Handle(GetRevenueByClassQuery request, CancellationToken cancellationToken)
    {
        var payments = await _paymentRepository.GetPaymentsByClassAsync(request.ClassId);
        var totalRevenue = payments
            .SelectMany(p => p.Transactions)
            .Sum(t => t.Amount);

        var totalDebt = payments.Sum(p => p.RemainingAmount);

        return new
        {
            classId = request.ClassId,
            totalRevenue,
            totalDebt,
            totalPayments = payments.Count(),
            students = payments.Select(p => new
            {
                p.StudentUserId,
                studentName = p.StudentUser?.FullName,
                p.TotalAmount,
                p.PaidAmount,
                p.RemainingAmount,
                p.Status
            })
        };
    }
}
