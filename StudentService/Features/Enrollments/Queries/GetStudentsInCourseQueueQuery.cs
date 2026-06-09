using MediatR;
using StudentService.DTOs;
using System.Collections.Generic;

namespace StudentService.Features.Enrollments.Queries;

public record GetStudentsInCourseQueueQuery(int CourseId) : IRequest<List<StudentDto>>;
