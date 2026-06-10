using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;

namespace StudentService.Features.Students.Commands;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, StudentDto?>
{
    private readonly IStudentRepository _studentRepository;

    public UpdateStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<StudentDto?> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByIdAsync(request.Id);
        if (student == null) return null;

        student.FullName = request.FullName;
        student.DateOfBirth = request.DateOfBirth;
        student.Gender = request.Gender;
        student.Phone = request.Phone;
        student.Email = request.Email;
        student.Address = request.Address;
        if (request.UserId.HasValue)
        {
            student.UserId = request.UserId.Value;
        }
        student.UpdatedAt = DateTime.UtcNow;

        _studentRepository.UpdateStudent(student);
        await _studentRepository.SaveChangesAsync();

        var enrollmentCount = await _studentRepository.GetEnrollmentCountAsync(request.Id);
        var resultDto = StudentMapper.MapToDto(student);
        resultDto.EnrollmentCount = enrollmentCount;
        return resultDto;
    }
}
