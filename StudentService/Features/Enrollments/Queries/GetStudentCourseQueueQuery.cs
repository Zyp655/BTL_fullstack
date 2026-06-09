using MediatR;
using System.Collections.Generic;

namespace StudentService.Features.Enrollments.Queries;

public record GetStudentCourseQueueQuery(int StudentId) : IRequest<List<int>>;
