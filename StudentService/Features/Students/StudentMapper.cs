using StudentService.DTOs;
using StudentService.Models;

namespace StudentService.Features.Students;

public static class StudentMapper
{
    public static StudentDto MapToDto(Student s) => new()
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
