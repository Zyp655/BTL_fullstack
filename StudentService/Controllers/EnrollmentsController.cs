using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.DTOs;
using StudentService.Models;

namespace StudentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly StudentDbContext _context;

    public EnrollmentsController(StudentDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Danh sách đăng ký (filter by classId, studentId)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedResult<EnrollmentDto>>> GetEnrollments(
        [FromQuery] int? classId,
        [FromQuery] int? studentId,
        [FromQuery] string? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
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

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(e => e.EnrolledAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new EnrollmentDto
            {
                EnrollmentId = e.EnrollmentId,
                StudentId = e.StudentId,
                StudentName = e.Student != null ? e.Student.FullName : null,
                ClassId = e.ClassId,
                Status = e.Status,
                EnrolledAt = e.EnrolledAt,
                CompletedAt = e.CompletedAt
            })
            .ToListAsync();

        return Ok(new PagedResult<EnrollmentDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        });
    }

    /// <summary>
    /// Đăng ký học viên vào lớp
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<EnrollmentDto>> CreateEnrollment(CreateEnrollmentDto dto)
    {
        var student = await _context.Students.FindAsync(dto.StudentId);
        if (student == null)
            return BadRequest(new { message = "Không tìm thấy học viên" });

        // Check duplicate enrollment
        if (await _context.Enrollments.AnyAsync(e => e.StudentId == dto.StudentId && e.ClassId == dto.ClassId && e.Status == "DangHoc"))
            return BadRequest(new { message = "Học viên đã đăng ký lớp này" });

        var enrollment = new Enrollment
        {
            StudentId = dto.StudentId,
            ClassId = dto.ClassId,
            Status = "DangHoc",
            EnrolledAt = DateTime.UtcNow
        };

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEnrollments), new EnrollmentDto
        {
            EnrollmentId = enrollment.EnrollmentId,
            StudentId = enrollment.StudentId,
            StudentName = student.FullName,
            ClassId = enrollment.ClassId,
            Status = enrollment.Status,
            EnrolledAt = enrollment.EnrolledAt
        });
    }

    /// <summary>
    /// Cập nhật trạng thái đăng ký
    /// </summary>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<EnrollmentDto>> UpdateEnrollmentStatus(int id, UpdateEnrollmentStatusDto dto)
    {
        var enrollment = await _context.Enrollments
            .Include(e => e.Student)
            .FirstOrDefaultAsync(e => e.EnrollmentId == id);

        if (enrollment == null)
            return NotFound(new { message = "Không tìm thấy đăng ký" });

        enrollment.Status = dto.Status;
        if (dto.Status == "HoanThanh")
            enrollment.CompletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new EnrollmentDto
        {
            EnrollmentId = enrollment.EnrollmentId,
            StudentId = enrollment.StudentId,
            StudentName = enrollment.Student?.FullName,
            ClassId = enrollment.ClassId,
            Status = enrollment.Status,
            EnrolledAt = enrollment.EnrolledAt,
            CompletedAt = enrollment.CompletedAt
        });
    }

    /// <summary>
    /// Danh sách học viên trong lớp (cho inter-service)
    /// </summary>
    [HttpGet("class/{classId}/students")]
    public async Task<ActionResult<List<StudentDto>>> GetStudentsByClass(int classId)
    {
        var students = await _context.Enrollments
            .Include(e => e.Student)
            .Where(e => e.ClassId == classId && e.Status == "DangHoc")
            .Select(e => new StudentDto
            {
                StudentId = e.Student!.StudentId,
                UserId = e.Student.UserId,
                FullName = e.Student.FullName,
                Email = e.Student.Email,
                Phone = e.Student.Phone,
                DateOfBirth = e.Student.DateOfBirth,
                Gender = e.Student.Gender,
                Address = e.Student.Address,
                CreatedAt = e.Student.CreatedAt,
                UpdatedAt = e.Student.UpdatedAt,
                EnrollmentCount = e.Student.Enrollments.Count
            })
            .ToListAsync();

        return Ok(students);
    }
}
