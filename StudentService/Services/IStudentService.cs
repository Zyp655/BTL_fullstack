using StudentService.DTOs;

namespace StudentService.Services;

public interface IStudentService
{
    Task<PagedResult<StudentDto>> GetStudentsAsync(string? search, string? gender, int page, int pageSize);
    Task<StudentDto?> GetStudentByIdAsync(int id);
    Task<StudentDto?> GetStudentByUserIdAsync(int userId);
    Task<StudentDto> CreateStudentAsync(CreateStudentDto dto);
    Task<StudentDto?> UpdateStudentAsync(int id, UpdateStudentDto dto);
    Task<List<EnrollmentDto>> GetStudentEnrollmentsAsync(int studentId);
}
