using FluentValidation;

namespace StudentService.Features.Students.Commands;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(100).WithMessage("Họ tên không được để trống và tối đa 100 ký tự");
        RuleFor(x => x.Gender).NotEmpty().Must(g => new[] { "Nam", "Nữ", "Khác" }.Contains(g)).WithMessage("Giới tính phải là Nam, Nữ hoặc Khác");
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Email không đúng định dạng");
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("UserId phải lớn hơn 0");
    }
}
