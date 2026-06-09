using FluentValidation;

namespace PaymentService.Features.Users.Commands;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id phải lớn hơn 0");
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(100).WithMessage("Họ tên không được để trống và tối đa 100 ký tự");
        RuleFor(x => x.Role).NotEmpty().Must(r => new[] { "Admin", "GiaoVien", "HocVien", "NhanVien" }.Contains(r)).WithMessage("Vai trò phải là Admin, GiaoVien, HocVien hoặc NhanVien");
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Email không đúng định dạng");
    }
}
