using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Models;

namespace StudentService.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly StudentDbContext _context;

    public EnrollmentRepository(StudentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsAsync(int? classId, int? studentId, string? status, int page, int pageSize)
    {
        var query = _context.Enrollments
            .Include(e => e.Student)
            .AsQueryable();

        if (classId.HasValue)
            query = query.Where(e => e.ClassId == classId.Value);

        if (studentId.HasValue)
            query = query.Where(e => e.StudentId == studentId.Value);

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(e => e.Status == status);

        return await query
            .OrderByDescending(e => e.EnrolledAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetEnrollmentsCountAsync(int? classId, int? studentId, string? status)
    {
        var query = _context.Enrollments.AsQueryable();

        if (classId.HasValue)
            query = query.Where(e => e.ClassId == classId.Value);

        if (studentId.HasValue)
            query = query.Where(e => e.StudentId == studentId.Value);

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(e => e.Status == status);

        return await query.CountAsync();
    }

    public async Task<Enrollment?> GetEnrollmentByIdAsync(int id)
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .FirstOrDefaultAsync(e => e.EnrollmentId == id);
    }

    public async Task<bool> HasActiveEnrollmentAsync(int studentId, int classId)
    {
        return await _context.Enrollments
            .AnyAsync(e => e.StudentId == studentId && e.ClassId == classId && e.Status == "DangHoc");
    }

    public async Task AddEnrollmentAsync(Enrollment enrollment)
    {
        await _context.Enrollments.AddAsync(enrollment);
    }

    public void UpdateEnrollment(Enrollment enrollment)
    {
        _context.Enrollments.Update(enrollment);
    }

    public async Task<IEnumerable<Student>> GetStudentsByClassAsync(int classId)
    {
        return await _context.Enrollments
            .Include(e => e.Student)
                .ThenInclude(s => s!.Enrollments)
            .Where(e => e.ClassId == classId && e.Status == "DangHoc")
            .Select(e => e.Student!)
            .ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
