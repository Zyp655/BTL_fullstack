using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Enrollments.Queries;

public record GetStudentCreditsQuery(int StudentId) : IRequest<StudentCreditSummaryDto>;
