using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;

namespace StudentService.Features.ExamResults.Queries;

public class GetResultsByEnrollmentQueryHandler : IRequestHandler<GetResultsByEnrollmentQuery, List<ExamResultDto>>
{
    private readonly IResultRepository _resultRepository;

    public GetResultsByEnrollmentQueryHandler(IResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }

    public async Task<List<ExamResultDto>> Handle(GetResultsByEnrollmentQuery request, CancellationToken cancellationToken)
    {
        var results = await _resultRepository.GetResultsByEnrollmentAsync(request.EnrollmentId);
        return results.Select(ExamResultMapper.MapToDto).ToList();
    }
}
