namespace Contracts;

public record SingleSessionRefundRequestEvent
{
    public int StudentUserId { get; init; }
    public int ClassId { get; init; }
}
