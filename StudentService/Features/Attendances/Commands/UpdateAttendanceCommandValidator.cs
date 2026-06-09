using FluentValidation;

namespace StudentService.Features.Attendances.Commands;

public class UpdateAttendanceCommandValidator : AbstractValidator<UpdateAttendanceCommand>
{
    public UpdateAttendanceCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id phải lớn hơn 0");
        RuleFor(x => x.Status).NotEmpty().WithMessage("Trạng thái không được để trống");
    }
}
