using StudentService.DTOs;

namespace StudentService.Services;

public interface IAttendanceService
{
    Task<List<AttendanceDto>> GetAttendancesByClassAsync(int classId);
    Task<List<AttendanceDto>> GetAttendancesByDateAsync(int classId, DateTime date);
    Task<List<AttendanceDto>> CreateAttendanceAsync(BatchAttendanceDto dto);
    Task<AttendanceDto?> UpdateAttendanceAsync(int id, CreateAttendanceDto dto);
    Task<List<AttendanceSummaryDto>> GetAttendanceSummaryAsync(int studentId);
}
