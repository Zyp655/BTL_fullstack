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

        var students = enrollments.Select(e => 
        {
            decimal attendanceScore = 10;
            if (e.Attendances.Any())
            {
                var total = e.Attendances.Count;
                var present = e.Attendances.Count(a => a.Status == "CoMat");
                var late = e.Attendances.Count(a => a.Status == "DiTre");
                var excused = e.Attendances.Count(a => a.Status == "CoPhep");
                
                attendanceScore = Math.Round((decimal)(present + excused + late) / total * 10, 1);
            }

            var gk = e.ExamResults.FirstOrDefault(r => r.ExamType == "GiuaKy")?.Score;
            var ck = e.ExamResults.FirstOrDefault(r => r.ExamType == "CuoiKy")?.Score;
            
            decimal? averageScore = null;
            if (gk.HasValue && ck.HasValue)
            {
                averageScore = Math.Round(attendanceScore * 0.1m + gk.Value * 0.3m + ck.Value * 0.6m, 2);
            }

            return new StudentResultDto
            {
                StudentId = e.StudentId,
                StudentName = e.Student?.FullName,
                Results = e.ExamResults.Select(ExamResultMapper.MapToDto).OrderBy(r => r.ExamDate).ToList(),
                AttendanceScore = attendanceScore,
                AverageScore = averageScore
            };
        }).OrderBy(s => s.StudentName).ToList();

        return new ClassResultSummaryDto
        {
            ClassId = request.ClassId,
            Students = students
        };
    }
}
