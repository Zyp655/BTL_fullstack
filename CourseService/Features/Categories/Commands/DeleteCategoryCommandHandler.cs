using MediatR;
using CourseService.Repositories;
using CourseService.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourseService.Features.Categories.Commands;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly CourseDbContext _context;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, CourseDbContext context)
    {
        _categoryRepository = categoryRepository;
        _context = context;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(request.Id);
        if (category == null) return false;

        // Check if any course is using this category code
        var isUsed = await _context.Courses.AnyAsync(c => c.Category == category.CategoryCode, cancellationToken);
        if (isUsed)
        {
            throw new InvalidOperationException("Không thể xóa danh mục này vì đang có khóa học đang sử dụng nó.");
        }

        _categoryRepository.DeleteCategory(category);
        return await _categoryRepository.SaveChangesAsync();
    }
}
