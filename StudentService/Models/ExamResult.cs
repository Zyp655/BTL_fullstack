using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentService.Models;

public class ExamResult
{
    [Key]
    public int ResultId { get; set; }

    [Required]
    public int EnrollmentId { get; set; }

    [MaxLength(30)]
    public string ExamType { get; set; } = "KiemTra"; // GiuaKy, CuoiKy, KiemTra

    [Column(TypeName = "decimal(5,2)")]
    public decimal Score { get; set; }

    [MaxLength(500)]
    public string? Note { get; set; }

    public int? GradedByTeacherId { get; set; } // Reference ID

    public DateTime? ExamDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("EnrollmentId")]
    public Enrollment? Enrollment { get; set; }
}
