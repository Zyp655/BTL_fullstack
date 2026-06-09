using MediatR;

namespace PaymentService.Features.Payments.Queries;

public record GetRevenueByClassQuery(int ClassId) : IRequest<object>;
