using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Students.Queries;

public record GetStudentByUserIdQuery(int UserId) : IRequest<StudentDto?>;
