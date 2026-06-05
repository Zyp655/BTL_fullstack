using StudentService.DTOs;
using StudentService.Models;
using StudentService.Repositories;

namespace StudentService.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IStudentRepository _studentRepository;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository, IStudentRepository studentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _studentRepository = studentRepository;
    }

    public async Task<PagedResult<EnrollmentDto>> GetEnrollmentsAsync(int? classId, int? studentId, string? status, int page, int pageSize)
    {
        var items = await _enrollmentRepository.GetEnrollmentsAsync(classId, studentId, status, page, pageSize);
        var totalCount = await _enrollmentRepository.GetEnrollmentsCountAsync(classId, studentId, status);

        return new PagedResult<EnrollmentDto>
        {
            Items = items.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<EnrollmentDto> CreateEnrollmentAsync(CreateEnrollmentDto dto)
    {
        var student = await _studentRepository.GetStudentByIdAsync(dto.StudentId);
        if (student == null)
            throw new KeyNotFoundException("Không tìm thấy học viên");

        if (await _enrollmentRepository.HasActiveEnrollmentAsync(dto.StudentId, dto.ClassId))
            throw new ArgumentException("Học viên đã đăng ký lớp này");

        var enrollment = new Enrollment
        {
            StudentId = dto.StudentId,
            ClassId = dto.ClassId,
            Status = "DangHoc",
            EnrolledAt = DateTime.UtcNow
        };

        await _enrollmentRepository.AddEnrollmentAsync(enrollment);
        await _enrollmentRepository.SaveChangesAsync();

        var result = MapToDto(enrollment);
        result.StudentName = student.FullName;
        return result;
    }

    public async Task<EnrollmentDto?> UpdateEnrollmentStatusAsync(int id, string status)
    {
        var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(id);
        if (enrollment == null) return null;

        enrollment.Status = status;
        if (status == "HoanThanh")
            enrollment.CompletedAt = DateTime.UtcNow;

        _enrollmentRepository.UpdateEnrollment(enrollment);
        await _enrollmentRepository.SaveChangesAsync();

        return MapToDto(enrollment);
    }

    public async Task<List<StudentDto>> GetStudentsByClassAsync(int classId)
    {
        var students = await _enrollmentRepository.GetStudentsByClassAsync(classId);
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

    private static EnrollmentDto MapToDto(Enrollment e) => new()
    {
        EnrollmentId = e.EnrollmentId,
        StudentId = e.StudentId,
        StudentName = e.Student?.FullName,
        ClassId = e.ClassId,
        Status = e.Status,
        EnrolledAt = e.EnrolledAt,
        CompletedAt = e.CompletedAt
    };
}
