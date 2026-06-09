using MediatR;
using PaymentService.DTOs;

namespace PaymentService.Features.Payments.Queries;

public record GetPaymentsByStudentQuery(int UserId) : IRequest<List<PaymentDto>>;
