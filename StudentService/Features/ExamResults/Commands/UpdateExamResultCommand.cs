using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.ExamResults.Commands;

public record UpdateExamResultCommand(
    int Id,
    decimal Score,
    string? Note
) : IRequest<ExamResultDto?>;
