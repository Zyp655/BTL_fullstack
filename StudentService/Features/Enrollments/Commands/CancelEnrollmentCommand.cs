using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace StudentService.Features.Enrollments.Commands;

public record CancelEnrollmentCommand(int StudentId, int ClassId) : IRequest<bool>;

public class CancelEnrollmentCommandHandler : IRequestHandler<CancelEnrollmentCommand, bool>
{
    private readonly StudentDbContext _context;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public CancelEnrollmentCommandHandler(StudentDbContext context, IEnrollmentRepository enrollmentRepository)
    {
        _context = context;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<bool> Handle(CancelEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == request.StudentId && e.ClassId == request.ClassId, cancellationToken);
            
        if (enrollment == null)
            return false;

        _enrollmentRepository.DeleteEnrollment(enrollment);
        return await _enrollmentRepository.SaveChangesAsync();
    }
}
