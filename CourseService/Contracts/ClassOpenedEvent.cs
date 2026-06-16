using System;

namespace Contracts;

public record ClassOpenedEvent
{
    public int ClassId { get; init; }
    public int CourseId { get; init; }
    public string ClassName { get; init; } = string.Empty;
    public string CourseName { get; init; } = string.Empty;
    public int? TeacherId { get; init; }
    public string? TeacherName { get; init; }
    public int? TeacherId2 { get; init; }
    public string? TeacherName2 { get; init; }
    public DateTime? StartDate { get; init; }
}
