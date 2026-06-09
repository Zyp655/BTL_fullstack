using MediatR;
using PaymentService.Repositories;

namespace PaymentService.Features.Payments.Queries;

public class GetRevenueByCourseQueryHandler : IRequestHandler<GetRevenueByCourseQuery, object>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetRevenueByCourseQueryHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<object> Handle(GetRevenueByCourseQuery request, CancellationToken cancellationToken)
    {
        var payments = await _paymentRepository.GetAllPaymentsWithClassAsync();
        var totalRevenue = payments
            .SelectMany(p => p.Transactions)
            .Sum(t => t.Amount);

        return new
        {
            courseId = request.CourseId,
            totalRevenue,
            totalPayments = payments.Count(),
            totalStudents = payments.Select(p => p.StudentUserId).Distinct().Count()
        };
    }
}
