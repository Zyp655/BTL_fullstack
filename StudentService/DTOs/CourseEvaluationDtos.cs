using System;
using System.Collections.Generic;

namespace StudentService.DTOs;

public class CreateCourseEvaluationDto
{
    public int CourseId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}

public class CourseEvaluationDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CourseEvaluationSummaryDto
{
    public int CourseId { get; set; }
    public double AverageRating { get; set; }
    public int TotalReviews { get; set; }
    public Dictionary<int, int> RatingDistribution { get; set; } = new();
    public List<CourseEvaluationDto> Reviews { get; set; } = new();
}
