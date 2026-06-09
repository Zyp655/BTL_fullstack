using FluentValidation;

namespace PaymentService.Features.Payments.Commands;

public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(x => x.StudentUserId).GreaterThan(0).WithMessage("StudentUserId phải lớn hơn 0");
        RuleFor(x => x.ClassId).NotEqual(0).WithMessage("ClassId không được bằng 0");
        RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("Số tiền phải lớn hơn 0");
        RuleFor(x => x.DueDate).NotEmpty().WithMessage("Hạn thanh toán không được để trống");
    }
}
