using FluentValidation;

namespace StudentService.Features.Enrollments.Commands;

public class UpdateEnrollmentStatusCommandValidator : AbstractValidator<UpdateEnrollmentStatusCommand>
{
    public UpdateEnrollmentStatusCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id phải lớn hơn 0");
        RuleFor(x => x.Status).NotEmpty().WithMessage("Trạng thái không được để trống");
    }
}
