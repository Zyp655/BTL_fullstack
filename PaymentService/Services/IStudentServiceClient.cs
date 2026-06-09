namespace PaymentService.Services;

public interface IStudentServiceClient
{
    Task<StudentInfoDto?> GetStudentByUserId(int userId);
    Task<List<StudentInfoDto>> GetStudentsByClassId(int classId);
}
