using PaymentService.DTOs;

namespace PaymentService.Services;

public interface ICourseServiceClient
{
    Task<ClassInfoDto?> GetClassInfo(int classId);
    Task<CourseInfoDto?> GetCourseInfo(int courseId);
    Task<List<ClassInfoDto>> GetClassesByTeacher(int teacherId);
}
