using MediatR;
using CourseService.DTOs;
using System.Collections.Generic;

namespace CourseService.Features.Categories.Queries;

public record GetCategoriesQuery() : IRequest<IEnumerable<CategoryDto>>;
