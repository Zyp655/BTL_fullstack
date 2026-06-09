using StudentService.DTOs;
using StudentService.Models;

namespace StudentService.Features.ExamResults;

public static class ExamResultMapper
{
    public static ExamResultDto MapToDto(ExamResult r) => new()
    {
        ResultId = r.ResultId,
        EnrollmentId = r.EnrollmentId,
        StudentId = r.Enrollment?.StudentId ?? 0,
        StudentName = r.Enrollment?.Student?.FullName,
        ExamType = r.ExamType,
        Score = r.Score,
        Note = r.Note,
        GradedByTeacherId = r.GradedByTeacherId,
        ExamDate = r.ExamDate,
        CreatedAt = r.CreatedAt
    };
}
