using System;
using System.ComponentModel.DataAnnotations;

namespace StudentService.Models;

public class SupportMessage
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int StudentId { get; set; }

    public Student Student { get; set; } = null!;

    [Required]
    [MaxLength(1000)]
    public string Message { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Pending"; // Pending, Resolved, Rejected

    public int? FromClassId { get; set; }
    
    public int? ToClassId { get; set; }

    [MaxLength(100)]
    public string? FromClassName { get; set; }

    [MaxLength(100)]
    public string? ToClassName { get; set; }

    [MaxLength(500)]
    public string? AdminResponse { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
