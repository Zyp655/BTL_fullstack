using FluentValidation;

namespace StudentService.Features.Students.Commands;

public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(100).WithMessage("Họ tên không được để trống và tối đa 100 ký tự");
        RuleFor(x => x.Gender).NotEmpty().Must(g => new[] { "Nam", "Nữ", "Khác" }.Contains(g)).WithMessage("Giới tính phải là Nam, Nữ hoặc Khác");
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Email không đúng định dạng");
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id phải lớn hơn 0");
    }
}
