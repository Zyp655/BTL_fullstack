using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    [Required]
    [MaxLength(20)]
    public string Role { get; set; } = "HocVien"; // Admin, GiaoVien, HocVien

    [MaxLength(200)]
    public string? Specialization { get; set; }

    [MaxLength(100)]
    public string? Degree { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
