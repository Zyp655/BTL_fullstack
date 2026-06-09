using PaymentService.DTOs;
using PaymentService.Models;

namespace PaymentService.Repositories;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetPaymentsAsync(string? status, int? studentUserId, string? search, int page, int pageSize);
    Task<int> GetPaymentsCountAsync(string? status, int? studentUserId, string? search);
    Task<IEnumerable<Payment>> GetPaymentsByStudentAsync(int userId);
    Task<Payment?> GetPaymentByIdAsync(int id);
    Task AddPaymentAsync(Payment payment);
    void UpdatePayment(Payment payment);
    Task<IEnumerable<PaymentTransaction>> GetTransactionsByPaymentIdAsync(int paymentId);
    Task AddTransactionAsync(PaymentTransaction transaction);
    Task<IEnumerable<Payment>> GetDebtsAsync();
    
    // Reporting queries
    Task<decimal> SumTransactionAmountAsync(int? year, int? month);
    Task<decimal> SumRemainingPaymentsAsync();
    Task<int> CountPaymentsAsync();
    Task<int> CountCompletedPaymentsAsync(string status = "HoanTat");
    Task<List<MonthlyRevenueDto>> GetMonthlyRevenuesAsync(int year);
    Task<IEnumerable<Payment>> GetPaymentsByClassAsync(int classId);
    Task<IEnumerable<Payment>> GetAllPaymentsWithClassAsync();
    
    Task<bool> SaveChangesAsync();
}
