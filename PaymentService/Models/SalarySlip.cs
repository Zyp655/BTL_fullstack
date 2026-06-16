using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentService.Models;

public class SalarySlip
{
    [Key]
    public int SalarySlipId { get; set; }

    [Required]
    public int TeacherId { get; set; }

    [Required]
    public int Month { get; set; }

    [Required]
    public int Year { get; set; }

    public decimal BaseSalary { get; set; }

    public decimal RatePerSession { get; set; }

    public int SessionsTaught { get; set; }

    public int TotalStudentSessions { get; set; }

    public decimal StudentAllowanceRate { get; set; }

    public decimal CalculatedSalary { get; set; }

    public decimal Bonus { get; set; } = 0;

    public decimal Deductions { get; set; } = 0;

    public decimal TotalAmount { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Pending"; // Pending, Approved, Paid

    [MaxLength(500)]
    public string? Notes { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("TeacherId")]
    public User? Teacher { get; set; }
}
