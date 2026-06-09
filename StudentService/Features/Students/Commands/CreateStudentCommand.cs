using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Students.Commands;

public record CreateStudentCommand(
    int UserId,
    string FullName,
    DateTime DateOfBirth,
    string Gender,
    string? Phone,
    string? Email,
    string? Address
) : IRequest<StudentDto>;
