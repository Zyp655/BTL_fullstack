using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;

namespace StudentService.Features.Enrollments.Queries;

public class GetStudentsByClassQueryHandler : IRequestHandler<GetStudentsByClassQuery, List<StudentDto>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public GetStudentsByClassQueryHandler(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<List<StudentDto>> Handle(GetStudentsByClassQuery request, CancellationToken cancellationToken)
    {
        var students = await _enrollmentRepository.GetStudentsByClassAsync(request.ClassId);
        return students.Select(s => new StudentDto
        {
            StudentId = s.StudentId,
            UserId = s.UserId,
            FullName = s.FullName,
            Email = s.Email,
            Phone = s.Phone,
            DateOfBirth = s.DateOfBirth,
            Gender = s.Gender,
            Address = s.Address,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt,
            EnrollmentCount = s.Enrollments?.Count ?? 0
        }).ToList();
    }
}
