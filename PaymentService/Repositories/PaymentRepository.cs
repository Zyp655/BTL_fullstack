using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.DTOs;
using PaymentService.Models;

namespace PaymentService.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly PaymentDbContext _context;

    public PaymentRepository(PaymentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Payment>> GetPaymentsAsync(string? status, int? studentUserId, string? search, int page, int pageSize)
    {
        var query = _context.Payments
            .Include(p => p.StudentUser)
            .Include(p => p.Transactions)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(p => p.Status == status);

        if (studentUserId.HasValue)
            query = query.Where(p => p.StudentUserId == studentUserId.Value);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.StudentUser != null && p.StudentUser.FullName.Contains(search));

        return await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetPaymentsCountAsync(string? status, int? studentUserId, string? search)
    {
        var query = _context.Payments.AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(p => p.Status == status);

        if (studentUserId.HasValue)
            query = query.Where(p => p.StudentUserId == studentUserId.Value);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.StudentUser != null && p.StudentUser.FullName.Contains(search));

        return await query.CountAsync();
    }

    public async Task<IEnumerable<Payment>> GetPaymentsByStudentAsync(int userId)
    {
        return await _context.Payments
            .Include(p => p.StudentUser)
            .Include(p => p.Transactions)
            .Where(p => p.StudentUserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<Payment?> GetPaymentByIdAsync(int id)
    {
        return await _context.Payments
            .Include(p => p.StudentUser)
            .Include(p => p.Transactions)
            .FirstOrDefaultAsync(p => p.PaymentId == id);
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
    }

    public void UpdatePayment(Payment payment)
    {
        _context.Payments.Update(payment);
    }

    public async Task<IEnumerable<PaymentTransaction>> GetTransactionsByPaymentIdAsync(int paymentId)
    {
        return await _context.PaymentTransactions
            .Where(t => t.PaymentId == paymentId)
            .OrderByDescending(t => t.PaidAt)
            .ToListAsync();
    }

    public async Task AddTransactionAsync(PaymentTransaction transaction)
    {
        await _context.PaymentTransactions.AddAsync(transaction);
    }

    public async Task<IEnumerable<Payment>> GetDebtsAsync()
    {
        return await _context.Payments
            .Include(p => p.StudentUser)
            .Where(p => p.RemainingAmount > 0)
            .OrderByDescending(p => p.RemainingAmount)
            .ToListAsync();
    }

    public async Task<decimal> SumTransactionAmountAsync(int? year, int? month)
    {
        var query = _context.PaymentTransactions.AsQueryable();

        if (year.HasValue)
            query = query.Where(t => t.PaidAt.Year == year.Value);

        if (month.HasValue)
            query = query.Where(t => t.PaidAt.Month == month.Value);

        return await query.SumAsync(t => t.Amount);
    }

    public async Task<decimal> SumRemainingPaymentsAsync()
    {
        return await _context.Payments.SumAsync(p => p.RemainingAmount);
    }

    public async Task<int> CountPaymentsAsync()
    {
        return await _context.Payments.CountAsync();
    }

    public async Task<int> CountCompletedPaymentsAsync(string status = "HoanTat")
    {
        return await _context.Payments.CountAsync(p => p.Status == status);
    }

    public async Task<List<MonthlyRevenueDto>> GetMonthlyRevenuesAsync(int year)
    {
        return await _context.PaymentTransactions
            .Where(t => t.PaidAt.Year == year)
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
    }

    public async Task<IEnumerable<Payment>> GetPaymentsByClassAsync(int classId)
    {
        return await _context.Payments
            .Include(p => p.Transactions)
            .Include(p => p.StudentUser)
            .Where(p => p.ClassId == classId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetAllPaymentsWithClassAsync()
    {
        return await _context.Payments
            .Include(p => p.Transactions)
            .Include(p => p.StudentUser)
            .Where(p => p.ClassId > 0)
            .ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
