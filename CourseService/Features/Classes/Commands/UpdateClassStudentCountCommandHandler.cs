using MediatR;
using CourseService.Repositories;
using CourseService.Common.Exceptions;

namespace CourseService.Features.Classes.Commands;

public class UpdateClassStudentCountCommandHandler : IRequestHandler<UpdateClassStudentCountCommand, bool>
{
    private readonly IClassRepository _classRepository;

    public UpdateClassStudentCountCommandHandler(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<bool> Handle(UpdateClassStudentCountCommand request, CancellationToken cancellationToken)
    {
        var cls = await _classRepository.GetClassByIdAsync(request.Id);
        if (cls == null)
            throw new NotFoundException("Lớp học", request.Id);

        int newCount = cls.CurrentStudents + request.Delta;
        if (newCount < 0)
        {
            newCount = 0;
        }

        if (request.Delta > 0 && newCount > cls.MaxStudents)
        {
            throw new ArgumentException($"Lớp học '{cls.ClassName}' đã đạt sĩ số tối đa ({cls.MaxStudents} học viên).");
        }

        cls.CurrentStudents = newCount;
        _classRepository.UpdateClass(cls);
        await _classRepository.SaveChangesAsync();

        return true;
    }
}
