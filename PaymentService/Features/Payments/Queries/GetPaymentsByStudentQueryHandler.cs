using MediatR;
using PaymentService.DTOs;
using PaymentService.Repositories;
using PaymentService.Services;

namespace PaymentService.Features.Payments.Queries;

public class GetPaymentsByStudentQueryHandler : IRequestHandler<GetPaymentsByStudentQuery, List<PaymentDto>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ICourseServiceClient _courseServiceClient;

    public GetPaymentsByStudentQueryHandler(IPaymentRepository paymentRepository, ICourseServiceClient courseServiceClient)
    {
        _paymentRepository = paymentRepository;
        _courseServiceClient = courseServiceClient;
    }

    public async Task<List<PaymentDto>> Handle(GetPaymentsByStudentQuery request, CancellationToken cancellationToken)
    {
        var payments = await _paymentRepository.GetPaymentsByStudentAsync(request.UserId);
        var dtos = payments.Select(PaymentMapper.MapToDto).ToList();

        // Enrich with ClassName and CourseName
        var classCache = new Dictionary<int, ClassInfoDto>();
        foreach (var dto in dtos)
        {
            if (dto.ClassId < 0)
            {
                var courseId = -dto.ClassId;
                var courseInfo = await _courseServiceClient.GetCourseInfo(courseId);
                if (courseInfo != null)
                {
                    dto.ClassName = "Chờ xếp lớp";
                    dto.CourseName = courseInfo.CourseName;
                }
            }
            else if (dto.ClassId > 0)
            {
                if (!classCache.TryGetValue(dto.ClassId, out var classInfo))
                {
                    classInfo = await _courseServiceClient.GetClassInfo(dto.ClassId);
                    classCache[dto.ClassId] = classInfo!;
                }

                if (classInfo != null)
                {
                    dto.ClassName = classInfo.ClassName;
                    dto.CourseName = classInfo.CourseName;
                }
            }
        }

        return dtos;
    }
}
