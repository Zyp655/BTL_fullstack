namespace PaymentService.Services;

public interface IStudentServiceClient
{
    Task<StudentInfoDto?> GetStudentByUserId(int userId);
    Task<List<StudentInfoDto>> GetStudentsByClassId(int classId);
    Task<TeacherAttendanceStatsDto?> GetAttendanceStats(List<int> classIds, int month, int year);
}

public class TeacherAttendanceStatsDto
{
    public int SessionsTaught { get; set; }
    public int TotalStudentSessions { get; set; }
}
