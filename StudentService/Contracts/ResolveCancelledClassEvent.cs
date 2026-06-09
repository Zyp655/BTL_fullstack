using System.Collections.Generic;

namespace Contracts;

public record StudentResolutionItem
{
    public int StudentUserId { get; init; }
    public string Action { get; init; } = string.Empty; // BaoLuu, ChuyenLop, HoanTien
    public int? NewClassId { get; init; }
}

public record ResolveCancelledClassEvent
{
    public int ClassId { get; init; }
    public List<StudentResolutionItem> Resolutions { get; init; } = new();
}
