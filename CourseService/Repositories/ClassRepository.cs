using Microsoft.EntityFrameworkCore;
using CourseService.Data;
using CourseService.Models;

namespace CourseService.Repositories;

public class ClassRepository : IClassRepository
{
    private readonly CourseDbContext _context;

    public ClassRepository(CourseDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Class>> GetClassesAsync(int? courseId, int? teacherId, string? status, string? search, int page, int pageSize)
    {
        var query = _context.Classes
            .Include(c => c.Course)
            .Include(c => c.Schedules)
            .AsQueryable();

        if (courseId.HasValue)
            query = query.Where(c => c.CourseId == courseId.Value);

        if (teacherId.HasValue)
            query = query.Where(c => c.TeacherId == teacherId.Value);

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(c => c.Status == status);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => c.ClassName.Contains(search) || (c.TeacherName != null && c.TeacherName.Contains(search)));

        return await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetClassesCountAsync(int? courseId, int? teacherId, string? status, string? search)
    {
        var query = _context.Classes.AsQueryable();

        if (courseId.HasValue)
            query = query.Where(c => c.CourseId == courseId.Value);

        if (teacherId.HasValue)
            query = query.Where(c => c.TeacherId == teacherId.Value);

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(c => c.Status == status);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => c.ClassName.Contains(search) || (c.TeacherName != null && c.TeacherName.Contains(search)));

        return await query.CountAsync();
    }

    public async Task<Class?> GetClassByIdAsync(int id)
    {
        return await _context.Classes
            .Include(c => c.Course)
            .Include(c => c.Schedules)
            .FirstOrDefaultAsync(c => c.ClassId == id);
    }

    public async Task<IEnumerable<Class>> GetClassesByTeacherAsync(int teacherId)
    {
        return await _context.Classes
            .Include(c => c.Course)
            .Include(c => c.Schedules)
            .Where(c => c.TeacherId == teacherId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task AddClassAsync(Class cls)
    {
        await _context.Classes.AddAsync(cls);
    }

    public void UpdateClass(Class cls)
    {
        _context.Classes.Update(cls);
    }

    public void DeleteClass(Class cls)
    {
        _context.Classes.Remove(cls);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
