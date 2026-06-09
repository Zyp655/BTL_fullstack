using MediatR;
using CourseService.DTOs;
using CourseService.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace CourseService.Features.Categories.Queries;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(request.Id);
        if (category == null) return null;
        return CategoryMapper.MapToDto(category);
    }
}
