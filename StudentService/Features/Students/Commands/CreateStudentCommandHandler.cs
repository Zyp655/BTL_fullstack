using MediatR;
using StudentService.DTOs;
using StudentService.Models;
using StudentService.Repositories;

namespace StudentService.Features.Students.Commands;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentDto>
{
    private readonly IStudentRepository _studentRepository;

    public CreateStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        if (await _studentRepository.ExistsByUserIdAsync(request.UserId))
            throw new ArgumentException("Học viên với UserId này đã tồn tại");

        var student = new Student
        {
            UserId = request.UserId,
            FullName = request.FullName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Phone = request.Phone,
            Email = request.Email,
            Address = request.Address,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _studentRepository.AddStudentAsync(student);
        await _studentRepository.SaveChangesAsync();

        return StudentMapper.MapToDto(student);
    }
}
