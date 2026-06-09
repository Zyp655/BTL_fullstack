using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Students.Commands;

public record UpdateStudentCommand(
    int Id,
    string FullName,
    DateTime DateOfBirth,
    string Gender,
    string? Phone,
    string? Email,
    string? Address
) : IRequest<StudentDto?>;
