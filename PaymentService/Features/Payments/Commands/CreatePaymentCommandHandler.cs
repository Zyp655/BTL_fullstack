using MediatR;
using PaymentService.DTOs;
using PaymentService.Models;
using PaymentService.Repositories;

namespace PaymentService.Features.Payments.Commands;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentDto>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUserRepository _userRepository;

    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IUserRepository userRepository)
    {
        _paymentRepository = paymentRepository;
        _userRepository = userRepository;
    }

    public async Task<PaymentDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var student = await _userRepository.GetUserByIdAsync(request.StudentUserId);
        if (student == null || student.Role != "HocVien")
            throw new KeyNotFoundException("Không tìm thấy học viên");

        var payment = new Payment
        {
            StudentUserId = request.StudentUserId,
            ClassId = request.ClassId,
            TotalAmount = request.TotalAmount,
            PaidAmount = 0,
            RemainingAmount = request.TotalAmount,
            Status = "ChuaTT",
            DueDate = request.DueDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _paymentRepository.AddPaymentAsync(payment);
        await _paymentRepository.SaveChangesAsync();

        var resultDto = PaymentMapper.MapToDto(payment);
        resultDto.StudentName = student.FullName;
        return resultDto;
    }
}
