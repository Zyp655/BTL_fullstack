using StudentService.DTOs;
using StudentService.Models;

namespace StudentService.Features.Attendances;

public static class AttendanceMapper
{
    public static AttendanceDto MapToDto(Attendance a) => new()
    {
        AttendanceId = a.AttendanceId,
        EnrollmentId = a.EnrollmentId,
        StudentId = a.Enrollment?.StudentId ?? 0,
        StudentName = a.Enrollment?.Student?.FullName,
        SessionDate = a.SessionDate,
        Status = a.Status,
        Note = a.Note,
        MarkedByTeacherId = a.MarkedByTeacherId,
        CreatedAt = a.CreatedAt
    };
}
