using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;

namespace StudentService.Features.Students.Queries;

public class GetStudentByUserIdQueryHandler : IRequestHandler<GetStudentByUserIdQuery, StudentDto?>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentByUserIdQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<StudentDto?> Handle(GetStudentByUserIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByUserIdAsync(request.UserId);
        if (student == null) return null;

        return StudentMapper.MapToDto(student);
    }
}
