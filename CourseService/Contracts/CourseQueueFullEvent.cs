using System;
using System.Collections.Generic;

namespace Contracts;

public record CourseQueueFullEvent
{
    public int CourseId { get; init; }
    public string CourseName { get; init; } = string.Empty;
    public List<int> StudentIds { get; init; } = new();
    public List<int> StudentUserIds { get; init; } = new();
}
