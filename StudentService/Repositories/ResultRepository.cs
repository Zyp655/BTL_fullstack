using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Models;

namespace StudentService.Repositories;

public class ResultRepository : IResultRepository
{
    private readonly StudentDbContext _context;

    public ResultRepository(StudentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExamResult>> GetResultsByEnrollmentAsync(int enrollmentId)
    {
        return await _context.ExamResults
            .Include(r => r.Enrollment)
                .ThenInclude(e => e!.Student)
            .Where(r => r.EnrollmentId == enrollmentId)
            .OrderByDescending(r => r.ExamDate)
            .ToListAsync();
    }

    public async Task<ExamResult?> GetResultByIdAsync(int id)
    {
        return await _context.ExamResults
            .Include(r => r.Enrollment)
                .ThenInclude(e => e!.Student)
            .FirstOrDefaultAsync(r => r.ResultId == id);
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsWithResultsByClassAsync(int classId)
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.ExamResults)
            .Include(e => e.Attendances)
            .Where(e => e.ClassId == classId && e.Status == "DangHoc")
            .ToListAsync();
    }

    public async Task AddResultAsync(ExamResult result)
    {
        await _context.ExamResults.AddAsync(result);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
