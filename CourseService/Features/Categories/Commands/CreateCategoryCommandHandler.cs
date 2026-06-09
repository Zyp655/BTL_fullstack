using MediatR;
using CourseService.DTOs;
using CourseService.Models;
using CourseService.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourseService.Features.Categories.Commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        // Check if duplicate code exists
        var existing = await _categoryRepository.GetCategoryByCodeAsync(request.CategoryCode);
        if (existing != null)
        {
            throw new ArgumentException($"Mã danh mục '{request.CategoryCode}' đã tồn tại.");
        }

        var category = new Category
        {
            CategoryName = request.CategoryName,
            CategoryCode = request.CategoryCode
        };

        await _categoryRepository.AddCategoryAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return CategoryMapper.MapToDto(category);
    }
}
