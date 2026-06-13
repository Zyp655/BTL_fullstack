using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseService.Models;

public class Course
{
    [Key]
    public int CourseId { get; set; }

    [Required]
    [MaxLength(200)]
    public string CourseName { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    [MaxLength(50)]
    public string Level { get; set; } = "Beginner"; // Beginner, Intermediate, Advanced

    [MaxLength(50)]
    public string Category { get; set; } = "NgoaiNgu"; // NgoaiNgu, TinHoc, KyNang

    [Column(TypeName = "decimal(18,2)")]
    public decimal Fee { get; set; }

    public int TotalSessions { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<Class> Classes { get; set; } = new List<Class>();
}
