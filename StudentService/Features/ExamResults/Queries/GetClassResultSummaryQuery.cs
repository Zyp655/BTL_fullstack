using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.ExamResults.Queries;

public record GetClassResultSummaryQuery(int ClassId) : IRequest<ClassResultSummaryDto>;
