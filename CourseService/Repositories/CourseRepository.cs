using Microsoft.EntityFrameworkCore;
using CourseService.Data;
using CourseService.Models;

namespace CourseService.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly CourseDbContext _context;

    public CourseRepository(CourseDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetCoursesAsync(string? search, string? category, string? level, bool? isActive, int page, int pageSize)
    {
        var query = _context.Courses.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => c.CourseName.Contains(search) || (c.Description != null && c.Description.Contains(search)));

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(c => c.Category == category);

        if (!string.IsNullOrWhiteSpace(level))
            query = query.Where(c => c.Level == level);

        if (isActive.HasValue)
            query = query.Where(c => c.IsActive == isActive.Value);

        return await query
            .Include(c => c.Classes)
            .OrderByDescending(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetCoursesCountAsync(string? search, string? category, string? level, bool? isActive)
    {
        var query = _context.Courses.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => c.CourseName.Contains(search) || (c.Description != null && c.Description.Contains(search)));

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(c => c.Category == category);

        if (!string.IsNullOrWhiteSpace(level))
            query = query.Where(c => c.Level == level);

        if (isActive.HasValue)
            query = query.Where(c => c.IsActive == isActive.Value);

        return await query.CountAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Classes)
            .FirstOrDefaultAsync(c => c.CourseId == id);
    }

    public async Task AddCourseAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
    }

    public void UpdateCourse(Course course)
    {
        _context.Courses.Update(course);
    }

    public async Task<int> GetClassCountAsync(int courseId)
    {
        return await _context.Classes.CountAsync(c => c.CourseId == courseId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
