using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.DTOs;

namespace PaymentService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ReportsController : ControllerBase
{
    private readonly PaymentDbContext _context;

    public ReportsController(PaymentDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Báo cáo doanh thu (theo tháng)
    /// </summary>
    [HttpGet("revenue")]
    public async Task<ActionResult<RevenueReportDto>> GetRevenueReport(
        [FromQuery] int? year,
        [FromQuery] int? month)
    {
        var currentYear = year ?? DateTime.UtcNow.Year;

        var transactionsQuery = _context.PaymentTransactions.AsQueryable();

        if (year.HasValue)
            transactionsQuery = transactionsQuery.Where(t => t.PaidAt.Year == year.Value);

        if (month.HasValue)
            transactionsQuery = transactionsQuery.Where(t => t.PaidAt.Month == month.Value);

        var totalRevenue = await transactionsQuery.SumAsync(t => t.Amount);
        var totalDebt = await _context.Payments.SumAsync(p => p.RemainingAmount);
        var totalPayments = await _context.Payments.CountAsync();
        var completedPayments = await _context.Payments.CountAsync(p => p.Status == "HoanTat");

        // Monthly breakdown for current year
        var monthlyRevenues = await _context.PaymentTransactions
            .Where(t => t.PaidAt.Year == currentYear)
            .GroupBy(t => new { t.PaidAt.Year, t.PaidAt.Month })
            .Select(g => new MonthlyRevenueDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                Revenue = g.Sum(t => t.Amount),
                TransactionCount = g.Count()
            })
            .OrderBy(m => m.Month)
            .ToListAsync();

        // Add month names
        var monthNames = new[] { "", "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
                                 "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
        foreach (var m in monthlyRevenues)
            m.MonthName = monthNames[m.Month];

        return Ok(new RevenueReportDto
        {
            TotalRevenue = totalRevenue,
            TotalDebt = totalDebt,
            TotalPayments = totalPayments,
            CompletedPayments = completedPayments,
            MonthlyRevenues = monthlyRevenues
        });
    }

    /// <summary>
    /// Doanh thu theo khóa học
    /// </summary>
    [HttpGet("revenue/course/{courseId}")]
    public async Task<ActionResult> GetRevenueByCourse(int courseId)
    {
        // Since we don't have direct course info here, aggregate by classId
        var payments = await _context.Payments
            .Include(p => p.Transactions)
            .Include(p => p.StudentUser)
            .Where(p => p.ClassId > 0) // All payments with class association
            .ToListAsync();

        var totalRevenue = payments
            .SelectMany(p => p.Transactions)
            .Sum(t => t.Amount);

        return Ok(new
        {
            courseId,
            totalRevenue,
            totalPayments = payments.Count,
            totalStudents = payments.Select(p => p.StudentUserId).Distinct().Count()
        });
    }

    /// <summary>
    /// Doanh thu theo lớp
    /// </summary>
    [HttpGet("revenue/class/{classId}")]
    public async Task<ActionResult> GetRevenueByClass(int classId)
    {
        var payments = await _context.Payments
            .Include(p => p.Transactions)
            .Include(p => p.StudentUser)
            .Where(p => p.ClassId == classId)
            .ToListAsync();

        var totalRevenue = payments
            .SelectMany(p => p.Transactions)
            .Sum(t => t.Amount);

        var totalDebt = payments.Sum(p => p.RemainingAmount);

        return Ok(new
        {
            classId,
            totalRevenue,
            totalDebt,
            totalPayments = payments.Count,
            students = payments.Select(p => new
            {
                p.StudentUserId,
                studentName = p.StudentUser?.FullName,
                p.TotalAmount,
                p.PaidAmount,
                p.RemainingAmount,
                p.Status
            })
        });
    }

    /// <summary>
    /// Dashboard tổng quan
    /// </summary>
    [HttpGet("dashboard")]
    public async Task<ActionResult<DashboardDto>> GetDashboard()
    {
        var totalUsers = await _context.Users.CountAsync(u => u.IsActive);
        var totalStudents = await _context.Users.CountAsync(u => u.Role == "HocVien" && u.IsActive);
        var totalTeachers = await _context.Users.CountAsync(u => u.Role == "GiaoVien" && u.IsActive);

        var totalRevenue = await _context.PaymentTransactions.SumAsync(t => t.Amount);
        var totalDebt = await _context.Payments.SumAsync(p => p.RemainingAmount);
        var totalPayments = await _context.Payments.CountAsync();

        var currentYear = DateTime.UtcNow.Year;
        var recentRevenues = await _context.PaymentTransactions
            .Where(t => t.PaidAt.Year == currentYear)
            .GroupBy(t => new { t.PaidAt.Year, t.PaidAt.Month })
            .Select(g => new MonthlyRevenueDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                Revenue = g.Sum(t => t.Amount),
                TransactionCount = g.Count()
            })
            .OrderBy(m => m.Month)
            .ToListAsync();

        var monthNames = new[] { "", "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
                                 "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
        foreach (var m in recentRevenues)
            m.MonthName = monthNames[m.Month];

        return Ok(new DashboardDto
        {
            TotalUsers = totalUsers,
            TotalStudents = totalStudents,
            TotalTeachers = totalTeachers,
            TotalRevenue = totalRevenue,
            TotalDebt = totalDebt,
            TotalPayments = totalPayments,
            RecentRevenues = recentRevenues
        });
    }
}
