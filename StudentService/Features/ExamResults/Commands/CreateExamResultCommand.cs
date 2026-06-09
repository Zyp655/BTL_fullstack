using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.ExamResults.Commands;

public record CreateExamResultCommand(
    int EnrollmentId,
    string ExamType,
    decimal Score,
    string? Note,
    DateTime? ExamDate
) : IRequest<ExamResultDto>;
