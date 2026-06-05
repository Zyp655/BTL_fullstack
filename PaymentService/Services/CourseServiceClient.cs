using PaymentService.DTOs;

namespace PaymentService.Services;

public class CourseServiceClient
{
    private readonly HttpClient _httpClient;

    public CourseServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ClassInfoDto?> GetClassInfo(int classId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/classes/{classId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ClassInfoDto>();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<CourseInfoDto?> GetCourseInfo(int courseId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/courses/{courseId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CourseInfoDto>();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
}

// DTOs for inter-service communication
public class ClassInfoDto
{
    public int ClassId { get; set; }
    public int CourseId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int MaxStudents { get; set; }
    public int CurrentStudents { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class CourseInfoDto
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public decimal Fee { get; set; }
}
