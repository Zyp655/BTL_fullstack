using MediatR;
using PaymentService.DTOs;
using PaymentService.Repositories;
using PaymentService.Services;

namespace PaymentService.Features.Payments.Queries;

public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, PagedResult<PaymentDto>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ICourseServiceClient _courseServiceClient;

    public GetPaymentsQueryHandler(IPaymentRepository paymentRepository, ICourseServiceClient courseServiceClient)
    {
        _paymentRepository = paymentRepository;
        _courseServiceClient = courseServiceClient;
    }

    public async Task<PagedResult<PaymentDto>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
    {
        var items = await _paymentRepository.GetPaymentsAsync(request.Status, request.StudentUserId, request.Search, request.Page, request.PageSize);
        var totalCount = await _paymentRepository.GetPaymentsCountAsync(request.Status, request.StudentUserId, request.Search);

        var dtos = items.Select(PaymentMapper.MapToDto).ToList();

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

        return new PagedResult<PaymentDto>
        {
            Items = dtos,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
