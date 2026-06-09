using FluentValidation;

namespace StudentService.Features.Enrollments.Commands;

public class CreateEnrollmentCommandValidator : AbstractValidator<CreateEnrollmentCommand>
{
    public CreateEnrollmentCommandValidator()
    {
        RuleFor(x => x.StudentId).GreaterThan(0).WithMessage("StudentId phải lớn hơn 0");
        RuleFor(x => x.ClassId).GreaterThan(0).WithMessage("ClassId phải lớn hơn 0");
    }
}
