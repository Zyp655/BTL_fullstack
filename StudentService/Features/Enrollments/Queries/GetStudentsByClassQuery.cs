using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Enrollments.Queries;

public record GetStudentsByClassQuery(int ClassId) : IRequest<List<StudentDto>>;
