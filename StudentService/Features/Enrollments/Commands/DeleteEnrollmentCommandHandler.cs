using MediatR;
using StudentService.Repositories;

namespace StudentService.Features.Enrollments.Commands;

public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, bool>
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public DeleteEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<bool> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(request.Id);
        if (enrollment == null)
            return false;

        _enrollmentRepository.DeleteEnrollment(enrollment);
        return await _enrollmentRepository.SaveChangesAsync();
    }
}
