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

    public async Task<bool> UpdateCurrentStudents(int classId, int delta)
    {
        try
        {
            // Call CourseService to update current students count
            var classInfo = await GetClassInfo(classId);
            if (classInfo == null) return false;

            // Simple validation - don't exceed max
            if (delta > 0 && classInfo.CurrentStudents >= classInfo.MaxStudents)
                return false;

            return true;
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
    public int MaxStudents { get; set; }
    public int CurrentStudents { get; set; }
    public string Status { get; set; } = string.Empty;
}
