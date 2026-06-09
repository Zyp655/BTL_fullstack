using StudentService.DTOs;
using StudentService.Models;

namespace StudentService.Features.Enrollments;

public static class EnrollmentMapper
{
    public static EnrollmentDto MapToDto(Enrollment e) => new()
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
