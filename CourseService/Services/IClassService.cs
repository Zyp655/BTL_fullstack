using CourseService.DTOs;

namespace CourseService.Services;

public interface IClassService
{
    Task<PagedResult<ClassDto>> GetClassesAsync(int? courseId, int? teacherId, string? status, string? search, int page, int pageSize);
    Task<ClassDto?> GetClassByIdAsync(int id);
    Task<IEnumerable<ClassDto>> GetClassesByTeacherAsync(int teacherId);
    Task<ClassDto> CreateClassAsync(CreateClassDto dto);
    Task<ClassDto?> UpdateClassAsync(int id, UpdateClassDto dto);
    Task<ClassDto?> UpdateClassStatusAsync(int id, string status);
    Task<bool> DeleteClassAsync(int id);
}
