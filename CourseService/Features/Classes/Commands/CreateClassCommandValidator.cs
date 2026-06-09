using FluentValidation;

namespace CourseService.Features.Classes.Commands;

public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
{
    public CreateClassCommandValidator()
    {
        RuleFor(x => x.CourseId).GreaterThan(0).WithMessage("CourseId phải lớn hơn 0");
        RuleFor(x => x.ClassName).NotEmpty().WithMessage("Tên lớp không được để trống").MaximumLength(200);
        RuleFor(x => x.MaxStudents).GreaterThan(0).WithMessage("Sĩ số tối đa phải lớn hơn 0");
    }
}
