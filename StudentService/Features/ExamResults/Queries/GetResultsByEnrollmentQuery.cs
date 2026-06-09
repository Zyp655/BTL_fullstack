using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.ExamResults.Queries;

public record GetResultsByEnrollmentQuery(int EnrollmentId) : IRequest<List<ExamResultDto>>;
