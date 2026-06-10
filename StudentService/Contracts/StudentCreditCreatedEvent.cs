using System;

namespace Contracts;

public record StudentCreditCreatedEvent
{
    public int StudentUserId { get; init; }
    public decimal Amount { get; init; }
    public int SourceClassId { get; init; }
}
