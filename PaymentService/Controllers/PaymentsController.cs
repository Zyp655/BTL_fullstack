using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.DTOs;
using PaymentService.Models;

namespace PaymentService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly PaymentDbContext _context;

    public PaymentsController(PaymentDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Danh sách phiếu học phí (Admin)
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PagedResult<PaymentDto>>> GetPayments(
        [FromQuery] string? search,
        [FromQuery] string? status,
        [FromQuery] int? studentUserId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
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

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new PaymentDto
            {
                PaymentId = p.PaymentId,
                StudentUserId = p.StudentUserId,
                StudentName = p.StudentUser != null ? p.StudentUser.FullName : null,
                ClassId = p.ClassId,
                TotalAmount = p.TotalAmount,
                PaidAmount = p.PaidAmount,
                RemainingAmount = p.RemainingAmount,
                Status = p.Status,
                DueDate = p.DueDate,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                Transactions = p.Transactions.Select(t => new TransactionDto
                {
                    TransactionId = t.TransactionId,
                    PaymentId = t.PaymentId,
                    Amount = t.Amount,
                    PaymentMethod = t.PaymentMethod,
                    Note = t.Note,
                    ReceivedByUserId = t.ReceivedByUserId,
                    PaidAt = t.PaidAt
                }).OrderByDescending(t => t.PaidAt).ToList()
            })
            .ToListAsync();

        return Ok(new PagedResult<PaymentDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        });
    }

    /// <summary>
    /// Học phí của học viên
    /// </summary>
    [HttpGet("student/{userId}")]
    public async Task<ActionResult<List<PaymentDto>>> GetPaymentsByStudent(int userId)
    {
        // Self or Admin check
        var currentUserId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
        var currentRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        if (currentRole != "Admin" && currentUserId != userId)
            return Forbid();

        var payments = await _context.Payments
            .Include(p => p.StudentUser)
            .Include(p => p.Transactions)
            .Where(p => p.StudentUserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new PaymentDto
            {
                PaymentId = p.PaymentId,
                StudentUserId = p.StudentUserId,
                StudentName = p.StudentUser != null ? p.StudentUser.FullName : null,
                ClassId = p.ClassId,
                TotalAmount = p.TotalAmount,
                PaidAmount = p.PaidAmount,
                RemainingAmount = p.RemainingAmount,
                Status = p.Status,
                DueDate = p.DueDate,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                Transactions = p.Transactions.Select(t => new TransactionDto
                {
                    TransactionId = t.TransactionId,
                    PaymentId = t.PaymentId,
                    Amount = t.Amount,
                    PaymentMethod = t.PaymentMethod,
                    Note = t.Note,
                    ReceivedByUserId = t.ReceivedByUserId,
                    PaidAt = t.PaidAt
                }).OrderByDescending(t => t.PaidAt).ToList()
            })
            .ToListAsync();

        return Ok(payments);
    }

    /// <summary>
    /// Tạo phiếu thu học phí
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PaymentDto>> CreatePayment(CreatePaymentDto dto)
    {
        var student = await _context.Users.FindAsync(dto.StudentUserId);
        if (student == null || student.Role != "HocVien")
            return BadRequest(new { message = "Không tìm thấy học viên" });

        var payment = new Payment
        {
            StudentUserId = dto.StudentUserId,
            ClassId = dto.ClassId,
            TotalAmount = dto.TotalAmount,
            PaidAmount = 0,
            RemainingAmount = dto.TotalAmount,
            Status = "ChuaTT",
            DueDate = dto.DueDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPaymentsByStudent), new { userId = payment.StudentUserId }, new PaymentDto
        {
            PaymentId = payment.PaymentId,
            StudentUserId = payment.StudentUserId,
            StudentName = student.FullName,
            ClassId = payment.ClassId,
            TotalAmount = payment.TotalAmount,
            PaidAmount = payment.PaidAmount,
            RemainingAmount = payment.RemainingAmount,
            Status = payment.Status,
            DueDate = payment.DueDate,
            CreatedAt = payment.CreatedAt,
            UpdatedAt = payment.UpdatedAt
        });
    }

    /// <summary>
    /// Ghi nhận thanh toán
    /// </summary>
    [HttpPost("{id}/transactions")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TransactionDto>> AddTransaction(int id, CreateTransactionDto dto)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment == null)
            return NotFound(new { message = "Không tìm thấy phiếu học phí" });

        if (payment.Status == "HoanTat")
            return BadRequest(new { message = "Phiếu học phí đã hoàn tất" });

        if (dto.Amount > payment.RemainingAmount)
            return BadRequest(new { message = $"Số tiền thanh toán vượt quá số tiền còn lại ({payment.RemainingAmount:N0} VNĐ)" });

        var currentUserId = int.Parse(User.FindFirst("userId")?.Value ?? "0");

        var transaction = new PaymentTransaction
        {
            PaymentId = id,
            Amount = dto.Amount,
            PaymentMethod = dto.PaymentMethod,
            Note = dto.Note,
            ReceivedByUserId = currentUserId,
            PaidAt = DateTime.UtcNow
        };

        _context.PaymentTransactions.Add(transaction);

        // Update payment amounts
        payment.PaidAmount += dto.Amount;
        payment.RemainingAmount -= dto.Amount;
        payment.Status = payment.RemainingAmount <= 0 ? "HoanTat" : "DangTT";
        payment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new TransactionDto
        {
            TransactionId = transaction.TransactionId,
            PaymentId = transaction.PaymentId,
            Amount = transaction.Amount,
            PaymentMethod = transaction.PaymentMethod,
            Note = transaction.Note,
            ReceivedByUserId = transaction.ReceivedByUserId,
            PaidAt = transaction.PaidAt
        });
    }

    /// <summary>
    /// Lịch sử thanh toán của phiếu
    /// </summary>
    [HttpGet("{id}/transactions")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<TransactionDto>>> GetTransactions(int id)
    {
        var transactions = await _context.PaymentTransactions
            .Where(t => t.PaymentId == id)
            .OrderByDescending(t => t.PaidAt)
            .Select(t => new TransactionDto
            {
                TransactionId = t.TransactionId,
                PaymentId = t.PaymentId,
                Amount = t.Amount,
                PaymentMethod = t.PaymentMethod,
                Note = t.Note,
                ReceivedByUserId = t.ReceivedByUserId,
                PaidAt = t.PaidAt
            })
            .ToListAsync();

        return Ok(transactions);
    }

    /// <summary>
    /// Danh sách công nợ
    /// </summary>
    [HttpGet("debts")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<PaymentDto>>> GetDebts()
    {
        var debts = await _context.Payments
            .Include(p => p.StudentUser)
            .Where(p => p.RemainingAmount > 0)
            .OrderByDescending(p => p.RemainingAmount)
            .Select(p => new PaymentDto
            {
                PaymentId = p.PaymentId,
                StudentUserId = p.StudentUserId,
                StudentName = p.StudentUser != null ? p.StudentUser.FullName : null,
                ClassId = p.ClassId,
                TotalAmount = p.TotalAmount,
                PaidAmount = p.PaidAmount,
                RemainingAmount = p.RemainingAmount,
                Status = p.Status,
                DueDate = p.DueDate,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .ToListAsync();

        return Ok(debts);
    }
}
