using MediatR;
using PaymentService.DTOs;

namespace PaymentService.Features.Payments.Queries;

public record GetPaymentsQuery(
    string? Search,
    string? Status,
    int? StudentUserId,
    int Page = 1,
    int PageSize = 10
) : IRequest<PagedResult<PaymentDto>>;
