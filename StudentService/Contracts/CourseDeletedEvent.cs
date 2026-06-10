using System;
using System.Collections.Generic;

namespace Contracts;

public record CourseDeletedEvent
{
    public int CourseId { get; init; }
    public List<int> ClassIds { get; init; } = new();
}
