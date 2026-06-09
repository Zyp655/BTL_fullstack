using FluentValidation;

namespace PaymentService.Features.Users.Commands;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id phải lớn hơn 0");
        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Mật khẩu hiện tại không được để trống");
        RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6).WithMessage("Mật khẩu mới không được để trống và tối thiểu 6 ký tự");
    }
}
