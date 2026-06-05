using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentService.Models;

public class Enrollment
{
    [Key]
    public int EnrollmentId { get; set; }

    [Required]
    public int StudentId { get; set; }

    public int ClassId { get; set; } // Reference ID from CourseService

    [MaxLength(20)]
    public string Status { get; set; } = "DangHoc"; // DangHoc, HoanThanh, HuyBo

    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    // Navigation
    [ForeignKey("StudentId")]
    public Student? Student { get; set; }

    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
}
