using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.DTOs;
using StudentService.Models;

namespace StudentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly StudentDbContext _context;

    public StudentsController(StudentDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Danh sách học viên (pagination)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedResult<StudentDto>>> GetStudents(
        [FromQuery] string? search,
        [FromQuery] string? gender,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = _context.Students.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(s => s.FullName.Contains(search) || (s.Email != null && s.Email.Contains(search)) || (s.Phone != null && s.Phone.Contains(search)));

        if (!string.IsNullOrWhiteSpace(gender))
            query = query.Where(s => s.Gender == gender);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(s => s.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new StudentDto
            {
                StudentId = s.StudentId,
                UserId = s.UserId,
                FullName = s.FullName,
                DateOfBirth = s.DateOfBirth,
                Gender = s.Gender,
                Phone = s.Phone,
                Email = s.Email,
                Address = s.Address,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                EnrollmentCount = s.Enrollments.Count
            })
            .ToListAsync();

        return Ok(new PagedResult<StudentDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        });
    }

    /// <summary>
    /// Chi tiết học viên
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDto>> GetStudent(int id)
    {
        var student = await _context.Students
            .Include(s => s.Enrollments)
            .FirstOrDefaultAsync(s => s.StudentId == id);

        if (student == null)
            return NotFound(new { message = "Không tìm thấy học viên" });

        return Ok(new StudentDto
        {
            StudentId = student.StudentId,
            UserId = student.UserId,
            FullName = student.FullName,
            DateOfBirth = student.DateOfBirth,
            Gender = student.Gender,
            Phone = student.Phone,
            Email = student.Email,
            Address = student.Address,
            CreatedAt = student.CreatedAt,
            UpdatedAt = student.UpdatedAt,
            EnrollmentCount = student.Enrollments.Count
        });
    }

    /// <summary>
    /// Tìm học viên theo UserId
    /// </summary>
    [HttpGet("by-user/{userId}")]
    public async Task<ActionResult<StudentDto>> GetStudentByUserId(int userId)
    {
        var student = await _context.Students
            .Include(s => s.Enrollments)
            .FirstOrDefaultAsync(s => s.UserId == userId);

        if (student == null)
            return NotFound(new { message = "Không tìm thấy học viên" });

        return Ok(new StudentDto
        {
            StudentId = student.StudentId,
            UserId = student.UserId,
            FullName = student.FullName,
            DateOfBirth = student.DateOfBirth,
            Gender = student.Gender,
            Phone = student.Phone,
            Email = student.Email,
            Address = student.Address,
            CreatedAt = student.CreatedAt,
            UpdatedAt = student.UpdatedAt,
            EnrollmentCount = student.Enrollments.Count
        });
    }

    /// <summary>
    /// Tạo hồ sơ học viên
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<StudentDto>> CreateStudent(CreateStudentDto dto)
    {
        if (await _context.Students.AnyAsync(s => s.UserId == dto.UserId))
            return BadRequest(new { message = "Học viên với UserId này đã tồn tại" });

        var student = new Student
        {
            UserId = dto.UserId,
            FullName = dto.FullName,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, new StudentDto
        {
            StudentId = student.StudentId,
            UserId = student.UserId,
            FullName = student.FullName,
            DateOfBirth = student.DateOfBirth,
            Gender = student.Gender,
            Phone = student.Phone,
            Email = student.Email,
            Address = student.Address,
            CreatedAt = student.CreatedAt,
            UpdatedAt = student.UpdatedAt,
            EnrollmentCount = 0
        });
    }

    /// <summary>
    /// Cập nhật hồ sơ học viên
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<StudentDto>> UpdateStudent(int id, UpdateStudentDto dto)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound(new { message = "Không tìm thấy học viên" });

        student.FullName = dto.FullName;
        student.DateOfBirth = dto.DateOfBirth;
        student.Gender = dto.Gender;
        student.Phone = dto.Phone;
        student.Email = dto.Email;
        student.Address = dto.Address;
        student.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var enrollmentCount = await _context.Enrollments.CountAsync(e => e.StudentId == id);

        return Ok(new StudentDto
        {
            StudentId = student.StudentId,
            UserId = student.UserId,
            FullName = student.FullName,
            DateOfBirth = student.DateOfBirth,
            Gender = student.Gender,
            Phone = student.Phone,
            Email = student.Email,
            Address = student.Address,
            CreatedAt = student.CreatedAt,
            UpdatedAt = student.UpdatedAt,
            EnrollmentCount = enrollmentCount
        });
    }

    /// <summary>
    /// Khóa học của học viên
    /// </summary>
    [HttpGet("{id}/enrollments")]
    public async Task<ActionResult<List<EnrollmentDto>>> GetStudentEnrollments(int id)
    {
        var enrollments = await _context.Enrollments
            .Include(e => e.Student)
            .Where(e => e.StudentId == id)
            .OrderByDescending(e => e.EnrolledAt)
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

        return Ok(enrollments);
    }
}
