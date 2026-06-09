using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Students.Queries;

public record GetStudentEnrollmentsQuery(int StudentId) : IRequest<List<EnrollmentDto>>;
