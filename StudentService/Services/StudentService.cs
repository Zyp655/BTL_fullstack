using StudentService.DTOs;
using StudentService.Models;
using StudentService.Repositories;

namespace StudentService.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public StudentService(IStudentRepository studentRepository, IEnrollmentRepository enrollmentRepository)
    {
        _studentRepository = studentRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<PagedResult<StudentDto>> GetStudentsAsync(string? search, string? gender, int page, int pageSize)
    {
        var items = await _studentRepository.GetStudentsAsync(search, gender, page, pageSize);
        var totalCount = await _studentRepository.GetStudentsCountAsync(search, gender);

        return new PagedResult<StudentDto>
        {
            Items = items.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<StudentDto?> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);
        if (student == null) return null;

        return MapToDto(student);
    }

    public async Task<StudentDto?> GetStudentByUserIdAsync(int userId)
    {
        var student = await _studentRepository.GetStudentByUserIdAsync(userId);
        if (student == null) return null;

        return MapToDto(student);
    }

    public async Task<StudentDto> CreateStudentAsync(CreateStudentDto dto)
    {
        if (await _studentRepository.ExistsByUserIdAsync(dto.UserId))
            throw new ArgumentException("Học viên với UserId này đã tồn tại");

        var student = new Student
        {
            UserId = dto.UserId,
            FullName = dto.FullName,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _studentRepository.AddStudentAsync(student);
        await _studentRepository.SaveChangesAsync();

        return MapToDto(student);
    }

    public async Task<StudentDto?> UpdateStudentAsync(int id, UpdateStudentDto dto)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);
        if (student == null) return null;

        student.FullName = dto.FullName;
        student.DateOfBirth = dto.DateOfBirth;
        student.Gender = dto.Gender;
        student.Phone = dto.Phone;
        student.Email = dto.Email;
        student.Address = dto.Address;
        student.UpdatedAt = DateTime.UtcNow;

        _studentRepository.UpdateStudent(student);
        await _studentRepository.SaveChangesAsync();

        var enrollmentCount = await _studentRepository.GetEnrollmentCountAsync(id);
        var resultDto = MapToDto(student);
        resultDto.EnrollmentCount = enrollmentCount;
        return resultDto;
    }

    public async Task<List<EnrollmentDto>> GetStudentEnrollmentsAsync(int studentId)
    {
        var enrollments = await _enrollmentRepository.GetEnrollmentsAsync(classId: null, studentId: studentId, status: null, page: 1, pageSize: 9999);
        return enrollments.Select(e => new EnrollmentDto
        {
            EnrollmentId = e.EnrollmentId,
            StudentId = e.StudentId,
            StudentName = e.Student?.FullName,
            ClassId = e.ClassId,
            Status = e.Status,
            EnrolledAt = e.EnrolledAt,
            CompletedAt = e.CompletedAt
        }).ToList();
    }

    private static StudentDto MapToDto(Student s) => new()
    {
        StudentId = s.StudentId,
        UserId = s.UserId,
        FullName = s.FullName,
        DateOfBirth = s.DateOfBirth,
        Gender = s.Gender,
        Phone = s.Phone,
        Email = s.Email,
        Address = s.Address,
        CreatedAt = s.CreatedAt,
        UpdatedAt = s.UpdatedAt,
        EnrollmentCount = s.Enrollments?.Count ?? 0
    };
}
