using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.DTOs;
using StudentService.Models;

namespace StudentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResultsController : ControllerBase
{
    private readonly StudentDbContext _context;

    public ResultsController(StudentDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Kết quả của enrollment
    /// </summary>
    [HttpGet("enrollment/{enrollmentId}")]
    public async Task<ActionResult<List<ExamResultDto>>> GetResultsByEnrollment(int enrollmentId)
    {
        var results = await _context.ExamResults
            .Include(r => r.Enrollment)
                .ThenInclude(e => e!.Student)
            .Where(r => r.EnrollmentId == enrollmentId)
            .OrderByDescending(r => r.ExamDate)
            .Select(r => new ExamResultDto
            {
                ResultId = r.ResultId,
                EnrollmentId = r.EnrollmentId,
                StudentId = r.Enrollment!.StudentId,
                StudentName = r.Enrollment.Student != null ? r.Enrollment.Student.FullName : null,
                ExamType = r.ExamType,
                Score = r.Score,
                Note = r.Note,
                GradedByTeacherId = r.GradedByTeacherId,
                ExamDate = r.ExamDate,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync();

        return Ok(results);
    }

    /// <summary>
    /// Nhập điểm
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ExamResultDto>> CreateResult(CreateExamResultDto dto)
    {
        var enrollment = await _context.Enrollments
            .Include(e => e.Student)
            .FirstOrDefaultAsync(e => e.EnrollmentId == dto.EnrollmentId);

        if (enrollment == null)
            return BadRequest(new { message = "Không tìm thấy đăng ký" });

        var result = new ExamResult
        {
            EnrollmentId = dto.EnrollmentId,
            ExamType = dto.ExamType,
            Score = dto.Score,
            Note = dto.Note,
            ExamDate = dto.ExamDate ?? DateTime.UtcNow,
            GradedByTeacherId = null, // Could be extracted from JWT
            CreatedAt = DateTime.UtcNow
        };

        _context.ExamResults.Add(result);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetResultsByEnrollment), new { enrollmentId = result.EnrollmentId }, new ExamResultDto
        {
            ResultId = result.ResultId,
            EnrollmentId = result.EnrollmentId,
            StudentId = enrollment.StudentId,
            StudentName = enrollment.Student?.FullName,
            ExamType = result.ExamType,
            Score = result.Score,
            Note = result.Note,
            GradedByTeacherId = result.GradedByTeacherId,
            ExamDate = result.ExamDate,
            CreatedAt = result.CreatedAt
        });
    }

    /// <summary>
    /// Sửa điểm
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ExamResultDto>> UpdateResult(int id, UpdateExamResultDto dto)
    {
        var result = await _context.ExamResults
            .Include(r => r.Enrollment)
                .ThenInclude(e => e!.Student)
            .FirstOrDefaultAsync(r => r.ResultId == id);

        if (result == null)
            return NotFound(new { message = "Không tìm thấy kết quả" });

        result.Score = dto.Score;
        result.Note = dto.Note;

        await _context.SaveChangesAsync();

        return Ok(new ExamResultDto
        {
            ResultId = result.ResultId,
            EnrollmentId = result.EnrollmentId,
            StudentId = result.Enrollment!.StudentId,
            StudentName = result.Enrollment.Student?.FullName,
            ExamType = result.ExamType,
            Score = result.Score,
            Note = result.Note,
            GradedByTeacherId = result.GradedByTeacherId,
            ExamDate = result.ExamDate,
            CreatedAt = result.CreatedAt
        });
    }

    /// <summary>
    /// Tổng kết điểm lớp
    /// </summary>
    [HttpGet("class/{classId}/summary")]
    public async Task<ActionResult<ClassResultSummaryDto>> GetClassResultSummary(int classId)
    {
        var enrollments = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.ExamResults)
            .Where(e => e.ClassId == classId)
            .ToListAsync();

        var students = enrollments.Select(e => new StudentResultDto
        {
            StudentId = e.StudentId,
            StudentName = e.Student?.FullName,
            Results = e.ExamResults.Select(r => new ExamResultDto
            {
                ResultId = r.ResultId,
                EnrollmentId = r.EnrollmentId,
                StudentId = e.StudentId,
                StudentName = e.Student?.FullName,
                ExamType = r.ExamType,
                Score = r.Score,
                Note = r.Note,
                GradedByTeacherId = r.GradedByTeacherId,
                ExamDate = r.ExamDate,
                CreatedAt = r.CreatedAt
            }).OrderBy(r => r.ExamDate).ToList(),
            AverageScore = e.ExamResults.Any() ? Math.Round(e.ExamResults.Average(r => r.Score), 2) : null
        }).OrderBy(s => s.StudentName).ToList();

        return Ok(new ClassResultSummaryDto
        {
            ClassId = classId,
            Students = students
        });
    }
}
