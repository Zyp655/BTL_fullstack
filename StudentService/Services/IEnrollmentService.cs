using StudentService.DTOs;

namespace StudentService.Services;

public interface IEnrollmentService
{
    Task<PagedResult<EnrollmentDto>> GetEnrollmentsAsync(int? classId, int? studentId, string? status, int page, int pageSize);
    Task<EnrollmentDto> CreateEnrollmentAsync(CreateEnrollmentDto dto);
    Task<EnrollmentDto?> UpdateEnrollmentStatusAsync(int id, string status);
    Task<List<StudentDto>> GetStudentsByClassAsync(int classId);
}
