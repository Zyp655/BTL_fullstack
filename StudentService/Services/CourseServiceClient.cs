namespace StudentService.Services;

public class CourseServiceClient : ICourseServiceClient
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
            var response = await _httpClient.GetAsync($"/api/v1/classes/{classId}");
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
            var response = await _httpClient.GetAsync($"/api/v1/courses/{courseId}");
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

    public async Task<bool> UpdateCurrentStudents(int classId, int delta)
    {
        try
        {
            var response = await _httpClient.PutAsync($"/api/v1/classes/{classId}/students/count?delta={delta}", null);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
}

// DTO for inter-service communication
public class ClassInfoDto
{
    public int ClassId { get; set; }
    public int CourseId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int? TeacherId { get; set; }
    public int MaxStudents { get; set; }
    public int CurrentStudents { get; set; }
    public string Status { get; set; } = string.Empty;
}
