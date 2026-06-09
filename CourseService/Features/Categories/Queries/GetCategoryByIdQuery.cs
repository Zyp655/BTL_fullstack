using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Categories.Queries;

public record GetCategoryByIdQuery(int Id) : IRequest<CategoryDto?>;
