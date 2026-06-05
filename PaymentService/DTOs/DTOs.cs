namespace PaymentService.DTOs;

// ===== Auth DTOs =====
public class LoginDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Role { get; set; } = "HocVien"; // Admin, GiaoVien, HocVien
}

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public UserDto User { get; set; } = null!;
}

public class ChangePasswordDto
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

// ===== User DTOs =====
public class UserDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class UpdateUserDto
{
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Role { get; set; } = "HocVien";
}

// ===== Payment DTOs =====
public class CreatePaymentDto
{
    public int StudentUserId { get; set; }
    public int ClassId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime? DueDate { get; set; }
}

public class PaymentDto
{
    public int PaymentId { get; set; }
    public int StudentUserId { get; set; }
    public string? StudentName { get; set; }
    public int ClassId { get; set; }
    public string? ClassName { get; set; }
    public string? CourseName { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal RemainingAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<TransactionDto> Transactions { get; set; } = new();
}

// ===== Transaction DTOs =====
public class CreateTransactionDto
{
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = "TienMat"; // TienMat, ChuyenKhoan, TheTD
    public string? Note { get; set; }
}

public class TransactionDto
{
    public int TransactionId { get; set; }
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string? Note { get; set; }
    public int? ReceivedByUserId { get; set; }
    public string? ReceivedByName { get; set; }
    public DateTime PaidAt { get; set; }
}

// ===== Report DTOs =====
public class RevenueReportDto
{
    public decimal TotalRevenue { get; set; }
    public decimal TotalDebt { get; set; }
    public int TotalPayments { get; set; }
    public int CompletedPayments { get; set; }
    public List<MonthlyRevenueDto> MonthlyRevenues { get; set; } = new();
}

public class MonthlyRevenueDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public string MonthName { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public int TransactionCount { get; set; }
}

public class DashboardDto
{
    public int TotalUsers { get; set; }
    public int TotalStudents { get; set; }
    public int TotalTeachers { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalDebt { get; set; }
    public int TotalPayments { get; set; }
    public List<MonthlyRevenueDto> RecentRevenues { get; set; } = new();
}

// ===== Pagination =====
public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
