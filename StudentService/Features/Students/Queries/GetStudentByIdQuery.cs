using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Students.Queries;

public record GetStudentByIdQuery(int Id) : IRequest<StudentDto?>;
