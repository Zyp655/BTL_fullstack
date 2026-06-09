using FluentValidation;

namespace PaymentService.Features.Users.Commands;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(50).WithMessage("Tên đăng nhập không được để trống và tối đa 50 ký tự");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Mật khẩu không được để trống và tối thiểu 6 ký tự");
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(100).WithMessage("Họ tên không được để trống và tối đa 100 ký tự");
        RuleFor(x => x.Role).NotEmpty().Must(r => new[] { "Admin", "GiaoVien", "HocVien", "NhanVien" }.Contains(r)).WithMessage("Vai trò phải là Admin, GiaoVien, HocVien hoặc NhanVien");
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Email không đúng định dạng");
    }
}
