using MediatR;
using PaymentService.DTOs;
using PaymentService.Repositories;

namespace PaymentService.Features.Payments.Queries;

public class GetRevenueReportQueryHandler : IRequestHandler<GetRevenueReportQuery, RevenueReportDto>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetRevenueReportQueryHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<RevenueReportDto> Handle(GetRevenueReportQuery request, CancellationToken cancellationToken)
    {
        var currentYear = request.Year ?? DateTime.UtcNow.Year;

        var totalRevenue = await _paymentRepository.SumTransactionAmountAsync(request.Year, request.Month);
        var totalDebt = await _paymentRepository.SumRemainingPaymentsAsync();
        var totalPayments = await _paymentRepository.CountPaymentsAsync();
        var completedPayments = await _paymentRepository.CountCompletedPaymentsAsync();

        var monthlyRevenues = await _paymentRepository.GetMonthlyRevenuesAsync(currentYear);

        var monthNames = new[] { "", "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
                                 "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
        foreach (var m in monthlyRevenues)
            m.MonthName = monthNames[m.Month];

        return new RevenueReportDto
        {
            TotalRevenue = totalRevenue,
            TotalDebt = totalDebt,
            TotalPayments = totalPayments,
            CompletedPayments = completedPayments,
            MonthlyRevenues = monthlyRevenues
        };
    }
}
