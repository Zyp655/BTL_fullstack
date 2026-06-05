namespace StudentService.DTOs;

// ===== Student DTOs =====
public class CreateStudentDto
{
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
}

public class UpdateStudentDto
{
    public string FullName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
}

public class StudentDto
{
    public int StudentId { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int EnrollmentCount { get; set; }
}

// ===== Enrollment DTOs =====
public class CreateEnrollmentDto
{
    public int StudentId { get; set; }
    public int ClassId { get; set; }
}

public class EnrollmentDto
{
    public int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public string? StudentName { get; set; }
    public int ClassId { get; set; }
    public string? ClassName { get; set; }
    public string? CourseName { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime EnrolledAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

public class UpdateEnrollmentStatusDto
{
    public string Status { get; set; } = string.Empty; // DangHoc, HoanThanh, HuyBo
}

// ===== Attendance DTOs =====
public class CreateAttendanceDto
{
    public int EnrollmentId { get; set; }
    public string Status { get; set; } = "CoMat"; // CoMat, Vang, CoPhep, DiTre
    public string? Note { get; set; }
}

public class BatchAttendanceDto
{
    public int ClassId { get; set; }
    public DateTime SessionDate { get; set; }
    public List<CreateAttendanceDto> Attendances { get; set; } = new();
}

public class AttendanceDto
{
    public int AttendanceId { get; set; }
    public int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public string? StudentName { get; set; }
    public DateTime SessionDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Note { get; set; }
    public int? MarkedByTeacherId { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class AttendanceSummaryDto
{
    public int StudentId { get; set; }
    public string? StudentName { get; set; }
    public int TotalSessions { get; set; }
    public int Present { get; set; }
    public int Absent { get; set; }
    public int Late { get; set; }
    public int Excused { get; set; }
    public double AttendanceRate { get; set; }
}

// ===== ExamResult DTOs =====
public class CreateExamResultDto
{
    public int EnrollmentId { get; set; }
    public string ExamType { get; set; } = "KiemTra"; // GiuaKy, CuoiKy, KiemTra
    public decimal Score { get; set; }
    public string? Note { get; set; }
    public DateTime? ExamDate { get; set; }
}

public class UpdateExamResultDto
{
    public decimal Score { get; set; }
    public string? Note { get; set; }
}

public class ExamResultDto
{
    public int ResultId { get; set; }
    public int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public string? StudentName { get; set; }
    public string ExamType { get; set; } = string.Empty;
    public decimal Score { get; set; }
    public string? Note { get; set; }
    public int? GradedByTeacherId { get; set; }
    public DateTime? ExamDate { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ClassResultSummaryDto
{
    public int ClassId { get; set; }
    public string? ClassName { get; set; }
    public List<StudentResultDto> Students { get; set; } = new();
}

public class StudentResultDto
{
    public int StudentId { get; set; }
    public string? StudentName { get; set; }
    public List<ExamResultDto> Results { get; set; } = new();
    public decimal? AverageScore { get; set; }
}

// ===== Pagination =====
public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
