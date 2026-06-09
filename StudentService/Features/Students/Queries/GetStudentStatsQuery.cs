using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;

namespace StudentService.Features.Students.Queries;

public record GetStudentStatsQuery : IRequest<StudentStatsDto>;

public class GetStudentStatsQueryHandler : IRequestHandler<GetStudentStatsQuery, StudentStatsDto>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentStatsQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<StudentStatsDto> Handle(GetStudentStatsQuery request, CancellationToken cancellationToken)
    {
        var total = await _studentRepository.GetStudentsCountAsync(null, null);
        var male = await _studentRepository.GetStudentsCountAsync(null, "Nam");
        var female = await _studentRepository.GetStudentsCountAsync(null, "Nữ");
        
        // Count others (or total - male - female)
        var other = total - male - female;

        return new StudentStatsDto
        {
            TotalCount = total,
            MaleCount = male,
            FemaleCount = female,
            OtherCount = other
        };
    }
}
