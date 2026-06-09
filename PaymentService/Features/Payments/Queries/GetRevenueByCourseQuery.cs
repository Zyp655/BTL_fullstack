using MediatR;

namespace PaymentService.Features.Payments.Queries;

public record GetRevenueByCourseQuery(int CourseId) : IRequest<object>;
