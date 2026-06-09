using FluentValidation;

namespace StudentService.Features.ExamResults.Commands;

public class UpdateExamResultCommandValidator : AbstractValidator<UpdateExamResultCommand>
{
    public UpdateExamResultCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id phải lớn hơn 0");
        RuleFor(x => x.Score).InclusiveBetween(0, 10).WithMessage("Điểm số phải từ 0 đến 10");
    }
}
