using MediatR;
using StudentService.DTOs;
using StudentService.Models;
using StudentService.Repositories;
using StudentService.Services;
using MassTransit;

namespace StudentService.Features.Enrollments.Commands;

public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, EnrollmentDto>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ICourseServiceClient _courseServiceClient;

    public CreateEnrollmentCommandHandler(
        IEnrollmentRepository enrollmentRepository, 
        IStudentRepository studentRepository,
        IPublishEndpoint publishEndpoint,
        ICourseServiceClient courseServiceClient)
    {
        _enrollmentRepository = enrollmentRepository;
        _studentRepository = studentRepository;
        _publishEndpoint = publishEndpoint;
        _courseServiceClient = courseServiceClient;
    }

    public async Task<EnrollmentDto> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByIdAsync(request.StudentId);
        if (student == null)
            throw new KeyNotFoundException("Không tìm thấy học viên");

        // Validate class capacity and existence from CourseService
        var classInfo = await _courseServiceClient.GetClassInfo(request.ClassId);
        if (classInfo == null)
            throw new KeyNotFoundException("Không tìm thấy lớp học");

        if (classInfo.CurrentStudents >= classInfo.MaxStudents)
            throw new ArgumentException($"Lớp học '{classInfo.ClassName}' đã đạt sĩ số tối đa ({classInfo.MaxStudents} học viên).");

        if (await _enrollmentRepository.HasActiveEnrollmentAsync(request.StudentId, request.ClassId))
            throw new ArgumentException("Học viên đã đăng ký lớp này");

        var enrollment = new Enrollment
        {
            StudentId = request.StudentId,
            ClassId = request.ClassId,
            Status = "Pending",
            EnrolledAt = DateTime.UtcNow
        };

        await _enrollmentRepository.AddEnrollmentAsync(enrollment);
        await _enrollmentRepository.SaveChangesAsync();

        // Publish event to RabbitMQ for PaymentService to consume asynchronously
        await _publishEndpoint.Publish<Contracts.StudentEnrolledEvent>(new Contracts.StudentEnrolledEvent
        {
            StudentId = enrollment.StudentId,
            UserId = student.UserId,
            ClassId = enrollment.ClassId,
            EnrolledAt = enrollment.EnrolledAt
        }, cancellationToken);

        var result = EnrollmentMapper.MapToDto(enrollment);
        result.StudentName = student.FullName;
        result.ClassName = classInfo?.ClassName;
        result.CourseName = classInfo?.CourseName;
        return result;
    }
}
