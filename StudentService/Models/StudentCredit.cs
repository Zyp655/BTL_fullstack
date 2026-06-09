using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentService.Models;

public class StudentCredit
{
    [Key]
    public int CreditId { get; set; }

    [Required]
    public int StudentId { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    public int SourceClassId { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Available"; // Available, Used, Refunded

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("StudentId")]
    public Student? Student { get; set; }
}
