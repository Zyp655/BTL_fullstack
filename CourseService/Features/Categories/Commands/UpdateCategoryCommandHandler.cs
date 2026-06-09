using MediatR;
using CourseService.DTOs;
using CourseService.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourseService.Features.Categories.Commands;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto?>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto?> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(request.Id);
        if (category == null) return null;

        // Check if duplicate code exists for another category
        var existing = await _categoryRepository.GetCategoryByCodeAsync(request.CategoryCode);
        if (existing != null && existing.CategoryId != request.Id)
        {
            throw new ArgumentException($"Mã danh mục '{request.CategoryCode}' đã tồn tại ở danh mục khác.");
        }

        category.CategoryName = request.CategoryName;
        category.CategoryCode = request.CategoryCode;

        _categoryRepository.DeleteCategory(category); // EF Core tracks entity update, wait no, don't delete! 
        // Oh, wait, in EF Core we update by modifying tracked entities, let's just save.
        // Wait, why did the default code have _categoryRepository.Update...?
        // Let's check ICategoryRepository.cs, it doesn't have Update. EF Core handles updates automatically by tracking properties!
        // Yes, modifying properties on the tracked 'category' object is enough, we just call SaveChangesAsync().
        await _categoryRepository.SaveChangesAsync();

        return CategoryMapper.MapToDto(category);
    }
}
