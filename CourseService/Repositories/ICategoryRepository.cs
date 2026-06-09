using CourseService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseService.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<Category?> GetCategoryByCodeAsync(string code);
    Task AddCategoryAsync(Category category);
    void DeleteCategory(Category category);
    Task<bool> SaveChangesAsync();
}
