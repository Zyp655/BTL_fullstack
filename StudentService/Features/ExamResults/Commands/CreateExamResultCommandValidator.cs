using FluentValidation;

namespace StudentService.Features.ExamResults.Commands;

public class CreateExamResultCommandValidator : AbstractValidator<CreateExamResultCommand>
{
    public CreateExamResultCommandValidator()
    {
        RuleFor(x => x.EnrollmentId).GreaterThan(0).WithMessage("EnrollmentId phải lớn hơn 0");
        RuleFor(x => x.ExamType).NotEmpty().WithMessage("Loại kỳ thi không được để trống");
        RuleFor(x => x.Score).InclusiveBetween(0, 10).WithMessage("Điểm số phải từ 0 đến 10");
    }
}
