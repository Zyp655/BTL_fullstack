using FluentValidation;

namespace CourseService.Features.Courses.Commands;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage("Tên khóa học không được để trống")
            .MaximumLength(200).WithMessage("Tên khóa học tối đa 200 ký tự");

        RuleFor(x => x.Fee)
            .GreaterThan(0).WithMessage("Học phí phải lớn hơn 0");

        RuleFor(x => x.TotalSessions)
            .GreaterThan(0).WithMessage("Số buổi học phải lớn hơn 0");

        RuleFor(x => x.Level)
            .Must(l => new[] { "Beginner", "Intermediate", "Advanced" }.Contains(l))
            .WithMessage("Level phải là Beginner, Intermediate hoặc Advanced");

        RuleFor(x => x.Category)
            .Must(c => new[] { "NgoaiNgu", "TinHoc", "KyNang" }.Contains(c))
            .WithMessage("Category phải là NgoaiNgu, TinHoc hoặc KyNang");
    }
}
