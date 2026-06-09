using MediatR;
using CourseService.Repositories;
using CourseService.Common.Exceptions;

namespace CourseService.Features.Classes.Commands;

public class DeleteClassCommandHandler : IRequestHandler<DeleteClassCommand, bool>
{
    private readonly IClassRepository _classRepository;

    public DeleteClassCommandHandler(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<bool> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
    {
        var cls = await _classRepository.GetClassByIdAsync(request.Id);
        if (cls == null)
            throw new NotFoundException("Lớp học", request.Id);

        if (cls.CurrentStudents > 0)
            throw new InvalidOperationException("Không thể xóa lớp đang có học viên");

        _classRepository.DeleteClass(cls);
        return await _classRepository.SaveChangesAsync();
    }
}
