using MediatR;
using PaymentService.DTOs;
using PaymentService.Repositories;

namespace PaymentService.Features.Users.Queries;

public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPaymentRepository _paymentRepository;

    public GetDashboardQueryHandler(IUserRepository userRepository, IPaymentRepository paymentRepository)
    {
        _userRepository = userRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var totalUsers = await _userRepository.CountUsersAsync(activeOnly: true);
        var totalStudents = await _userRepository.CountUsersByRoleAsync("HocVien", activeOnly: true);
        var totalTeachers = await _userRepository.CountUsersByRoleAsync("GiaoVien", activeOnly: true);

        var totalRevenue = await _paymentRepository.SumTransactionAmountAsync(year: null, month: null);
        var totalDebt = await _paymentRepository.SumRemainingPaymentsAsync();
        var totalPayments = await _paymentRepository.CountPaymentsAsync();

        var currentYear = DateTime.UtcNow.Year;
        var recentRevenues = await _paymentRepository.GetMonthlyRevenuesAsync(currentYear);

        var monthNames = new[] { "", "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
                                 "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
        foreach (var m in recentRevenues)
            m.MonthName = monthNames[m.Month];

        return new DashboardDto
        {
            TotalUsers = totalUsers,
            TotalStudents = totalStudents,
            TotalTeachers = totalTeachers,
            TotalRevenue = totalRevenue,
            TotalDebt = totalDebt,
            TotalPayments = totalPayments,
            RecentRevenues = recentRevenues
        };
    }
}
