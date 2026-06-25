using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentService.Models;

public class Lesson
{
    [Key]
    public int LessonId { get; set; }

    [Required]
    public int ClassId { get; set; }

    [Required]
    public DateTime LessonDate { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? FileName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
