using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;
using StudentService.Services;

namespace StudentService.Features.Enrollments.Commands;

public class UpdateEnrollmentStatusCommandHandler : IRequestHandler<UpdateEnrollmentStatusCommand, EnrollmentDto?>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseServiceClient _courseServiceClient;

    public UpdateEnrollmentStatusCommandHandler(IEnrollmentRepository enrollmentRepository, ICourseServiceClient courseServiceClient)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseServiceClient = courseServiceClient;
    }

    public async Task<EnrollmentDto?> Handle(UpdateEnrollmentStatusCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(request.Id);
        if (enrollment == null) return null;

        enrollment.Status = request.Status;
        if (request.Status == "HoanThanh")
            enrollment.CompletedAt = DateTime.UtcNow;

        _enrollmentRepository.UpdateEnrollment(enrollment);
        await _enrollmentRepository.SaveChangesAsync();

        var result = EnrollmentMapper.MapToDto(enrollment);
        var classInfo = await _courseServiceClient.GetClassInfo(enrollment.ClassId);
        if (classInfo != null)
        {
            result.ClassName = classInfo.ClassName;
            result.CourseName = classInfo.CourseName;
        }
        return result;
    }
}
