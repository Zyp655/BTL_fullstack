namespace StudentService.Services;

public interface ICourseServiceClient
{
    Task<ClassInfoDto?> GetClassInfo(int classId);
    Task<bool> UpdateCurrentStudents(int classId, int delta);
}
