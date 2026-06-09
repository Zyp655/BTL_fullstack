using CourseService.DTOs;
using CourseService.Models;

namespace CourseService.Features.Categories;

public static class CategoryMapper
{
    public static CategoryDto MapToDto(Category c) => new()
    {
        CategoryId = c.CategoryId,
        CategoryName = c.CategoryName,
        CategoryCode = c.CategoryCode
    };
}
