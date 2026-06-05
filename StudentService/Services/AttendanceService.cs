using StudentService.DTOs;
using StudentService.Models;
using StudentService.Repositories;

namespace StudentService.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IStudentRepository _studentRepository;

    public AttendanceService(
        IAttendanceRepository attendanceRepository,
        IEnrollmentRepository enrollmentRepository,
        IStudentRepository studentRepository)
    {
        _attendanceRepository = attendanceRepository;
        _enrollmentRepository = enrollmentRepository;
        _studentRepository = studentRepository;
    }

    public async Task<List<AttendanceDto>> GetAttendancesByClassAsync(int classId)
    {
        var attendances = await _attendanceRepository.GetAttendancesByClassAsync(classId);
        return attendances.Select(MapToDto).ToList();
    }

    public async Task<List<AttendanceDto>> GetAttendancesByDateAsync(int classId, DateTime date)
    {
        var attendances = await _attendanceRepository.GetAttendancesByDateAsync(classId, date);
        return attendances.Select(MapToDto).ToList();
    }

    public async Task<List<AttendanceDto>> CreateAttendanceAsync(BatchAttendanceDto dto)
    {
        foreach (var item in dto.Attendances)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(item.EnrollmentId);
            if (enrollment == null) continue;

            var existing = await _attendanceRepository.GetAttendanceByEnrollmentAndDateAsync(item.EnrollmentId, dto.SessionDate);
            if (existing != null)
            {
                existing.Status = item.Status;
                existing.Note = item.Note;
                _attendanceRepository.UpdateAttendance(existing);
            }
            else
            {
                var attendance = new Attendance
                {
                    EnrollmentId = item.EnrollmentId,
                    SessionDate = dto.SessionDate,
                    Status = item.Status,
                    Note = item.Note,
                    MarkedByTeacherId = null,
                    CreatedAt = DateTime.UtcNow
                };
                await _attendanceRepository.AddAttendanceAsync(attendance);
            }
        }

        await _attendanceRepository.SaveChangesAsync();

        var reloaded = await _attendanceRepository.GetAttendancesByDateAsync(dto.ClassId, dto.SessionDate);
        return reloaded.Select(MapToDto).ToList();
    }

    public async Task<AttendanceDto?> UpdateAttendanceAsync(int id, CreateAttendanceDto dto)
    {
        var attendance = await _attendanceRepository.GetAttendanceByIdAsync(id);
        if (attendance == null) return null;

        attendance.Status = dto.Status;
        attendance.Note = dto.Note;

        _attendanceRepository.UpdateAttendance(attendance);
        await _attendanceRepository.SaveChangesAsync();

        return MapToDto(attendance);
    }

    public async Task<List<AttendanceSummaryDto>> GetAttendanceSummaryAsync(int studentId)
    {
        var student = await _studentRepository.GetStudentByIdAsync(studentId);
        if (student == null)
            throw new KeyNotFoundException("Không tìm thấy học viên");

        var enrollments = await _attendanceRepository.GetEnrollmentsWithAttendancesByStudentAsync(studentId);

        return enrollments.Select(e =>
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
    }

    private static AttendanceDto MapToDto(Attendance a) => new()
    {
        AttendanceId = a.AttendanceId,
        EnrollmentId = a.EnrollmentId,
        StudentId = a.Enrollment?.StudentId ?? 0,
        StudentName = a.Enrollment?.Student?.FullName,
        SessionDate = a.SessionDate,
        Status = a.Status,
        Note = a.Note,
        MarkedByTeacherId = a.MarkedByTeacherId,
        CreatedAt = a.CreatedAt
    };
}
