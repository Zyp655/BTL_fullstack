namespace CourseService.Validators;

public class ClassStatusValidator : IClassStatusValidator
{
    private static readonly Dictionary<string, List<string>> AllowedTransitions = new()
    {
        { "Opened", new List<string> { "Opened", "InProgress", "Cancelled" } },
        { "InProgress", new List<string> { "InProgress", "Completed", "Cancelled" } },
        { "Completed", new List<string> { "Completed" } }, // Final state
        { "Cancelled", new List<string> { "Cancelled" } }  // Final state
    };

    public bool CanTransition(string currentStatus, string targetStatus, out string? errorMessage)
    {
        var validStatuses = new[] { "Opened", "InProgress", "Completed", "Cancelled" };
        if (!validStatuses.Contains(targetStatus))
        {
            errorMessage = "Trạng thái không hợp lệ. Chọn: Opened, InProgress, Completed, Cancelled";
            return false;
        }

        if (AllowedTransitions.TryGetValue(currentStatus, out var targetStatuses))
        {
            if (targetStatuses.Contains(targetStatus))
            {
                errorMessage = null;
                return true;
            }
        }

        errorMessage = $"Không thể chuyển trạng thái từ {currentStatus} sang {targetStatus}";
        return false;
    }
}
