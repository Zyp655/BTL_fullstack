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
        // Filter out per-quiz detail records (Quiz_xxx), only show aggregated KiemTra result
        return results
            .Where(r => r.Note == null || !r.Note.StartsWith("Quiz_"))
            .Select(ExamResultMapper.MapToDto).ToList();
    }
}
