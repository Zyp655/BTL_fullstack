using System;

namespace Contracts;

public record StudentEnrolledEvent
{
    public int StudentId { get; init; }
    public int UserId { get; init; }
    public int ClassId { get; init; }
    public DateTime EnrolledAt { get; init; }
}
