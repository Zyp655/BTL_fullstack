using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentService.Models;

public class CourseQueue
{
    [Key]
    public int CourseQueueId { get; set; }

    [Required]
    public int StudentId { get; set; }

    [Required]
    public int CourseId { get; set; }

    public DateTime QueuedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("StudentId")]
    public Student? Student { get; set; }
}
