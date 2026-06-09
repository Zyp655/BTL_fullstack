using MediatR;
using PaymentService.DTOs;

namespace PaymentService.Features.Users.Queries;

public record GetDashboardQuery() : IRequest<DashboardDto>;
