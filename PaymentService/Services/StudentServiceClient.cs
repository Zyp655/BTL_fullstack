namespace PaymentService.Services;

public class StudentServiceClient : IStudentServiceClient
{
    private readonly HttpClient _httpClient;

    public StudentServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<StudentInfoDto?> GetStudentByUserId(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/v1/students/by-user/{userId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StudentInfoDto>();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<StudentInfoDto>> GetStudentsByClassId(int classId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/v1/enrollments/class/{classId}/students");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<StudentInfoDto>>() ?? new();
            }
            return new();
        }
        catch
        {
            return new();
        }
    }

    public async Task<TeacherAttendanceStatsDto?> GetAttendanceStats(List<int> classIds, int month, int year)
    {
        try
        {
            var requestBody = new { ClassIds = classIds, Month = month, Year = year };
            var response = await _httpClient.PostAsJsonAsync("/api/v1/attendances/stats", requestBody);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TeacherAttendanceStatsDto>();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
}

// DTO for inter-service communication
public class StudentInfoDto
{
    public int StudentId { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
