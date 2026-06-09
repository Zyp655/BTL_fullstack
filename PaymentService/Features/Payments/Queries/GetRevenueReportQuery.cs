using MediatR;
using PaymentService.DTOs;

namespace PaymentService.Features.Payments.Queries;

public record GetRevenueReportQuery(int? Year, int? Month) : IRequest<RevenueReportDto>;
