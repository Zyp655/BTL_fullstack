using MediatR;
using PaymentService.DTOs;

namespace PaymentService.Features.Payments.Queries;

public record GetDebtsQuery() : IRequest<List<PaymentDto>>;
