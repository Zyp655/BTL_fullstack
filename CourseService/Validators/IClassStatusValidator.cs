namespace CourseService.Validators;

public interface IClassStatusValidator
{
    bool CanTransition(string currentStatus, string targetStatus, out string? errorMessage);
}
