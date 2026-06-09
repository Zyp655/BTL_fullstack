using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Models;

namespace StudentService.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentDbContext _context;

    public StudentRepository(StudentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetStudentsAsync(string? search, string? gender, int page, int pageSize)
    {
        var query = _context.Students.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(s => s.FullName.Contains(search) || (s.Email != null && s.Email.Contains(search)) || (s.Phone != null && s.Phone.Contains(search)));

        if (!string.IsNullOrWhiteSpace(gender))
            query = query.Where(s => s.Gender == gender);

        return await query
            .Include(s => s.Enrollments)
            .OrderByDescending(s => s.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetStudentsCountAsync(string? search, string? gender)
    {
        var query = _context.Students.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(s => s.FullName.Contains(search) || (s.Email != null && s.Email.Contains(search)) || (s.Phone != null && s.Phone.Contains(search)));

        if (!string.IsNullOrWhiteSpace(gender))
            query = query.Where(s => s.Gender == gender);

        return await query.CountAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await _context.Students
            .Include(s => s.Enrollments)
            .FirstOrDefaultAsync(s => s.StudentId == id);
    }

    public async Task<Student?> GetStudentByUserIdAsync(int userId)
    {
        return await _context.Students
            .Include(s => s.Enrollments)
            .FirstOrDefaultAsync(s => s.UserId == userId);
    }

    public async Task<bool> ExistsByUserIdAsync(int userId)
    {
        return await _context.Students.AnyAsync(s => s.UserId == userId);
    }

    public async Task AddStudentAsync(Student student)
    {
        await _context.Students.AddAsync(student);
    }

    public void UpdateStudent(Student student)
    {
        _context.Students.Update(student);
    }

    public async Task<int> GetEnrollmentCountAsync(int studentId)
    {
        return await _context.Enrollments.CountAsync(e => e.StudentId == studentId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
