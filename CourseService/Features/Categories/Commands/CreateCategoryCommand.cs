using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Categories.Commands;

public record CreateCategoryCommand(
    string CategoryName,
    string CategoryCode
) : IRequest<CategoryDto>;
