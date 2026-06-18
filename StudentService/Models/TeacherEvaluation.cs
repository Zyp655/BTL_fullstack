using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentService.Models;

public class TeacherEvaluation
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int StudentId { get; set; }

    [ForeignKey("StudentId")]
    public Student? Student { get; set; }

    [Required]
    public int ClassId { get; set; } // Reference from CourseService

    [Required]
    public int TeacherId { get; set; } // Reference from CourseService/PaymentService

    [Required]
    [Range(1, 5)]
    public int TeachingQualityRating { get; set; } // 1 to 5 stars

    [Required]
    [Range(1, 5)]
    public int SupportRating { get; set; } // 1 to 5 stars

    [Required]
    [Range(1, 5)]
    public int CurriculumRating { get; set; } // 1 to 5 stars

    [Required]
    [Range(1, 5)]
    public int PunctualityRating { get; set; } // 1 to 5 stars

    [Required]
    public double Rating { get; set; } // Calculated average: (TQ + S + C + P) / 4.0

    [MaxLength(1000)]
    public string? Comment { get; set; }

    public string? DetailedRatingsJson { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
