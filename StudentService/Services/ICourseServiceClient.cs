namespace StudentService.Services;

public interface ICourseServiceClient
{
    Task<ClassInfoDto?> GetClassInfo(int classId);
    Task<CourseInfoDto?> GetCourseInfo(int courseId);
    Task<bool> UpdateCurrentStudents(int classId, int delta);
}

public class CourseInfoDto
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public decimal Fee { get; set; }
}
