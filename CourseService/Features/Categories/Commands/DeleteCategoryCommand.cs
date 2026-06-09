using MediatR;

namespace CourseService.Features.Categories.Commands;

public record DeleteCategoryCommand(int Id) : IRequest<bool>;
