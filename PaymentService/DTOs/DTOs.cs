using System.Text.Json.Serialization;

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
    public string? Specialization { get; set; }
    public string? Degree { get; set; }
}

public class SignUpDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
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
    public string? Specialization { get; set; }
    public string? Degree { get; set; }
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
    public string? Specialization { get; set; }
    public string? Degree { get; set; }
}

public class UpdateProfileDto
{
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

public class ForgotPasswordDto
{
    public string Email { get; set; } = string.Empty;
}

public class ResetPasswordWithOtpDto
{
    public string Email { get; set; } = string.Empty;
    public string Otp { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
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

// ===== Sepay Webhook DTO =====
public class SepayWebhookDto
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("gateway")]
    public string? Gateway { get; set; }

    [JsonPropertyName("transaction_date")]
    public string? TransactionDate { get; set; }

    [JsonPropertyName("transactionDate")]
    public string? TransactionDateCamel { get; set; }

    [JsonPropertyName("account_number")]
    public string? AccountNumber { get; set; }

    [JsonPropertyName("accountNumber")]
    public string? AccountNumberCamel { get; set; }

    [JsonPropertyName("amount_in")]
    public decimal? AmountIn { get; set; }

    [JsonPropertyName("transferAmount")]
    public decimal? TransferAmount { get; set; }

    [JsonPropertyName("amountIn")]
    public decimal? AmountInCamel { get; set; }

    [JsonPropertyName("amount_out")]
    public decimal? AmountOut { get; set; }

    [JsonPropertyName("amountOut")]
    public decimal? AmountOutCamel { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("transaction_content")]
    public string? TransactionContent { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("transactionContent")]
    public string? TransactionContentCamel { get; set; }

    [JsonPropertyName("reference_number")]
    public string? ReferenceNumber { get; set; }

    [JsonPropertyName("referenceCode")]
    public string? ReferenceCode { get; set; }

    [JsonPropertyName("referenceNumber")]
    public string? ReferenceNumberCamel { get; set; }
}

public class UserStatsDto
{
    public int TotalCount { get; set; }
    public int AdminCount { get; set; }
    public int TeacherCount { get; set; }
    public int StudentCount { get; set; }
}

