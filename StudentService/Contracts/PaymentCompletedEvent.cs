using System;

namespace Contracts;

public record PaymentCompletedEvent
{
    public int StudentUserId { get; init; }
    public int ClassId { get; init; }
    public decimal PaidAmount { get; init; }
    public DateTime PaidAt { get; init; }
}
