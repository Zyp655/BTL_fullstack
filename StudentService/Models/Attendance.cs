using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentService.Models;

public class Attendance
{
    [Key]
    public int AttendanceId { get; set; }

    [Required]
    public int EnrollmentId { get; set; }

    public DateTime SessionDate { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } = "CoMat"; // CoMat, Vang, CoPhep, DiTre

    [MaxLength(500)]
    public string? Note { get; set; }

    public int? MarkedByTeacherId { get; set; } // Reference ID

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("EnrollmentId")]
    public Enrollment? Enrollment { get; set; }
}
