using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Categories.Commands;

public record UpdateCategoryCommand(
    int Id,
    string CategoryName,
    string CategoryCode
) : IRequest<CategoryDto?>;
