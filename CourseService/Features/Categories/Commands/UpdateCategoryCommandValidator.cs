using FluentValidation;

namespace CourseService.Features.Categories.Commands;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryName)
            .NotEmpty().WithMessage("Tên danh mục không được để trống.")
            .MaximumLength(100).WithMessage("Tên danh mục không được dài quá 100 ký tự.");

        RuleFor(x => x.CategoryCode)
            .NotEmpty().WithMessage("Mã danh mục không được để trống.")
            .MaximumLength(50).WithMessage("Mã danh mục không được dài quá 50 ký tự.")
            .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Mã danh mục chỉ được chứa các ký tự chữ, số và dấu gạch dưới.");
    }
}
