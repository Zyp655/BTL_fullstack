using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.DTOs;
using StudentService.Models;

namespace StudentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendancesController : ControllerBase
{
    private readonly StudentDbContext _context;

    public AttendancesController(StudentDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Điểm danh của lớp
    /// </summary>
    [HttpGet("class/{classId}")]
    public async Task<ActionResult<List<AttendanceDto>>> GetAttendancesByClass(int classId)
    {
        var attendances = await _context.Attendances
            .Include(a => a.Enrollment)
                .ThenInclude(e => e!.Student)
            .Where(a => a.Enrollment!.ClassId == classId)
            .OrderByDescending(a => a.SessionDate)
            .Select(a => new AttendanceDto
            {
                AttendanceId = a.AttendanceId,
                EnrollmentId = a.EnrollmentId,
                StudentId = a.Enrollment!.StudentId,
                StudentName = a.Enrollment.Student != null ? a.Enrollment.Student.FullName : null,
                SessionDate = a.SessionDate,
                Status = a.Status,
                Note = a.Note,
                MarkedByTeacherId = a.MarkedByTeacherId,
                CreatedAt = a.CreatedAt
            })
            .ToListAsync();

        return Ok(attendances);
    }

    /// <summary>
    /// Điểm danh theo ngày
    /// </summary>
    [HttpGet("class/{classId}/date/{date}")]
    public async Task<ActionResult<List<AttendanceDto>>> GetAttendancesByDate(int classId, DateTime date)
    {
        var targetDate = date.Date;
        var attendances = await _context.Attendances
            .Include(a => a.Enrollment)
                .ThenInclude(e => e!.Student)
            .Where(a => a.Enrollment!.ClassId == classId && a.SessionDate.Date == targetDate)
            .Select(a => new AttendanceDto
            {
                AttendanceId = a.AttendanceId,
                EnrollmentId = a.EnrollmentId,
                StudentId = a.Enrollment!.StudentId,
                StudentName = a.Enrollment.Student != null ? a.Enrollment.Student.FullName : null,
                SessionDate = a.SessionDate,
                Status = a.Status,
                Note = a.Note,
                MarkedByTeacherId = a.MarkedByTeacherId,
                CreatedAt = a.CreatedAt
            })
            .ToListAsync();

        return Ok(attendances);
    }

    /// <summary>
    /// Điểm danh batch (nhiều học viên cùng lúc)
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<List<AttendanceDto>>> CreateAttendance(BatchAttendanceDto dto)
    {
        var results = new List<AttendanceDto>();

        foreach (var item in dto.Attendances)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .FirstOrDefaultAsync(e => e.EnrollmentId == item.EnrollmentId);

            if (enrollment == null) continue;

            // Check if already marked for this date
            var existing = await _context.Attendances
                .FirstOrDefaultAsync(a => a.EnrollmentId == item.EnrollmentId && a.SessionDate.Date == dto.SessionDate.Date);

            if (existing != null)
            {
                // Update existing
                existing.Status = item.Status;
                existing.Note = item.Note;
            }
            else
            {
                var attendance = new Attendance
                {
                    EnrollmentId = item.EnrollmentId,
                    SessionDate = dto.SessionDate,
                    Status = item.Status,
                    Note = item.Note,
                    MarkedByTeacherId = null, // Could be extracted from JWT
                    CreatedAt = DateTime.UtcNow
                };

                _context.Attendances.Add(attendance);
            }
        }

        await _context.SaveChangesAsync();

        // Return the attendance for this class & date
        var attendances = await _context.Attendances
            .Include(a => a.Enrollment)
                .ThenInclude(e => e!.Student)
            .Where(a => a.Enrollment!.ClassId == dto.ClassId && a.SessionDate.Date == dto.SessionDate.Date)
            .Select(a => new AttendanceDto
            {
                AttendanceId = a.AttendanceId,
                EnrollmentId = a.EnrollmentId,
                StudentId = a.Enrollment!.StudentId,
                StudentName = a.Enrollment.Student != null ? a.Enrollment.Student.FullName : null,
                SessionDate = a.SessionDate,
                Status = a.Status,
                Note = a.Note,
                MarkedByTeacherId = a.MarkedByTeacherId,
                CreatedAt = a.CreatedAt
            })
            .ToListAsync();

        return Ok(attendances);
    }

    /// <summary>
    /// Sửa điểm danh
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<AttendanceDto>> UpdateAttendance(int id, CreateAttendanceDto dto)
    {
        var attendance = await _context.Attendances
            .Include(a => a.Enrollment)
                .ThenInclude(e => e!.Student)
            .FirstOrDefaultAsync(a => a.AttendanceId == id);

        if (attendance == null)
            return NotFound(new { message = "Không tìm thấy bản ghi điểm danh" });

        attendance.Status = dto.Status;
        attendance.Note = dto.Note;

        await _context.SaveChangesAsync();

        return Ok(new AttendanceDto
        {
            AttendanceId = attendance.AttendanceId,
            EnrollmentId = attendance.EnrollmentId,
            StudentId = attendance.Enrollment!.StudentId,
            StudentName = attendance.Enrollment.Student?.FullName,
            SessionDate = attendance.SessionDate,
            Status = attendance.Status,
            Note = attendance.Note,
            MarkedByTeacherId = attendance.MarkedByTeacherId,
            CreatedAt = attendance.CreatedAt
        });
    }

    /// <summary>
    /// Tỷ lệ chuyên cần của học viên
    /// </summary>
    [HttpGet("student/{studentId}/summary")]
    public async Task<ActionResult<List<AttendanceSummaryDto>>> GetAttendanceSummary(int studentId)
    {
        var student = await _context.Students.FindAsync(studentId);
        if (student == null)
            return NotFound(new { message = "Không tìm thấy học viên" });

        var enrollments = await _context.Enrollments
            .Include(e => e.Attendances)
            .Where(e => e.StudentId == studentId)
            .ToListAsync();

        var summaries = enrollments.Select(e =>
        {
            var total = e.Attendances.Count;
            var present = e.Attendances.Count(a => a.Status == "CoMat");
            var absent = e.Attendances.Count(a => a.Status == "Vang");
            var late = e.Attendances.Count(a => a.Status == "DiTre");
            var excused = e.Attendances.Count(a => a.Status == "CoPhep");

            return new AttendanceSummaryDto
            {
                StudentId = studentId,
                StudentName = student.FullName,
                TotalSessions = total,
                Present = present,
                Absent = absent,
                Late = late,
                Excused = excused,
                AttendanceRate = total > 0 ? Math.Round((double)(present + late) / total * 100, 1) : 0
            };
        }).ToList();

        return Ok(summaries);
    }
}
