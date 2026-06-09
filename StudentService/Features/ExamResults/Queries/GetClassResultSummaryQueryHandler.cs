using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;

namespace StudentService.Features.ExamResults.Queries;

public class GetClassResultSummaryQueryHandler : IRequestHandler<GetClassResultSummaryQuery, ClassResultSummaryDto>
{
    private readonly IResultRepository _resultRepository;

    public GetClassResultSummaryQueryHandler(IResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }

    public async Task<ClassResultSummaryDto> Handle(GetClassResultSummaryQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await _resultRepository.GetEnrollmentsWithResultsByClassAsync(request.ClassId);

        var students = enrollments.Select(e => new StudentResultDto
        {
            StudentId = e.StudentId,
            StudentName = e.Student?.FullName,
            Results = e.ExamResults.Select(ExamResultMapper.MapToDto).OrderBy(r => r.ExamDate).ToList(),
            AverageScore = e.ExamResults.Any() ? Math.Round(e.ExamResults.Average(r => r.Score), 2) : null
        }).OrderBy(s => s.StudentName).ToList();

        return new ClassResultSummaryDto
        {
            ClassId = request.ClassId,
            Students = students
        };
    }
}
