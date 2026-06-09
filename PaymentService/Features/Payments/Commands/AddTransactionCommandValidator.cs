using FluentValidation;

namespace PaymentService.Features.Payments.Commands;

public class AddTransactionCommandValidator : AbstractValidator<AddTransactionCommand>
{
    public AddTransactionCommandValidator()
    {
        RuleFor(x => x.PaymentId).GreaterThan(0).WithMessage("PaymentId phải lớn hơn 0");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Số tiền thanh toán phải lớn hơn 0");
        RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("Phương thức thanh toán không được để trống");
    }
}
