using FluentValidation;

namespace StudentService.Features.Attendances.Commands;

public class CreateBatchAttendanceCommandValidator : AbstractValidator<CreateBatchAttendanceCommand>
{
    public CreateBatchAttendanceCommandValidator()
    {
        RuleFor(x => x.ClassId).GreaterThan(0).WithMessage("ClassId phải lớn hơn 0");
        RuleFor(x => x.SessionDate).NotEmpty().WithMessage("Ngày học không được để trống");
        RuleFor(x => x.Attendances).NotEmpty().WithMessage("Danh sách điểm danh không được để trống");
    }
}
