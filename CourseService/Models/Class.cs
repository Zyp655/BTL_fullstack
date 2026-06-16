using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseService.Models;

public class Class
{
    [Key]
    public int ClassId { get; set; }

    [Required]
    public int CourseId { get; set; }

    [Required]
    [MaxLength(100)]
    public string ClassName { get; set; } = string.Empty;

    public int? TeacherId { get; set; } // Reference ID from PaymentService User

    [MaxLength(100)]
    public string? TeacherName { get; set; }

    public int? TeacherId2 { get; set; }

    [MaxLength(100)]
    public string? TeacherName2 { get; set; }

    [MaxLength(50)]
    public string? Room { get; set; }

    public int MaxStudents { get; set; } = 30;

    public int CurrentStudents { get; set; } = 0;

    [MaxLength(20)]
    public string Status { get; set; } = "Opened"; // Opened, InProgress, Completed, Cancelled

    public int TotalSessions { get; set; } = 0;

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("CourseId")]
    public Course? Course { get; set; }

    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
