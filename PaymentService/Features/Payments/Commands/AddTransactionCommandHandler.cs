using MediatR;
using PaymentService.DTOs;
using PaymentService.Models;
using PaymentService.Repositories;
using MassTransit;

namespace PaymentService.Features.Payments.Commands;

public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, TransactionDto>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public AddTransactionCommandHandler(IPaymentRepository paymentRepository, IPublishEndpoint publishEndpoint)
    {
        _paymentRepository = paymentRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<TransactionDto> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetPaymentByIdAsync(request.PaymentId);
        if (payment == null)
            throw new KeyNotFoundException("Không tìm thấy phiếu học phí");

        if (payment.Status == "HoanTat")
            throw new InvalidOperationException("Phiếu học phí đã hoàn tất");

        if (request.Amount > payment.RemainingAmount)
            throw new ArgumentException($"Số tiền thanh toán vượt quá số tiền còn lại ({payment.RemainingAmount:N0} VNĐ)");

        var transaction = new PaymentTransaction
        {
            PaymentId = request.PaymentId,
            Amount = request.Amount,
            PaymentMethod = request.PaymentMethod,
            Note = request.Note,
            ReceivedByUserId = request.ReceivedByUserId,
            PaidAt = DateTime.UtcNow
        };

        await _paymentRepository.AddTransactionAsync(transaction);

        // Update payment amounts
        payment.PaidAmount += request.Amount;
        payment.RemainingAmount -= request.Amount;
        payment.Status = payment.RemainingAmount <= 0 ? "HoanTat" : "DangTT";
        payment.UpdatedAt = DateTime.UtcNow;

        _paymentRepository.UpdatePayment(payment);
        await _paymentRepository.SaveChangesAsync();

        // If fully paid, publish PaymentCompletedEvent
        if (payment.Status == "HoanTat")
        {
            await _publishEndpoint.Publish<Contracts.PaymentCompletedEvent>(new Contracts.PaymentCompletedEvent
            {
                StudentUserId = payment.StudentUserId,
                ClassId = payment.ClassId,
                PaidAmount = payment.PaidAmount,
                PaidAt = DateTime.UtcNow
            }, cancellationToken);
        }

        return new TransactionDto
        {
            TransactionId = transaction.TransactionId,
            PaymentId = transaction.PaymentId,
            Amount = transaction.Amount,
            PaymentMethod = transaction.PaymentMethod,
            Note = transaction.Note,
            ReceivedByUserId = transaction.ReceivedByUserId,
            PaidAt = transaction.PaidAt
        };
    }
}
