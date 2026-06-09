using MediatR;
using CourseService.DTOs;
using CourseService.Repositories;
using CourseService.Common.Exceptions;

namespace CourseService.Features.Classes.Queries;

public class GetClassByIdQueryHandler : IRequestHandler<GetClassByIdQuery, ClassDto>
{
    private readonly IClassRepository _classRepository;

    public GetClassByIdQueryHandler(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<ClassDto> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
    {
        var cls = await _classRepository.GetClassByIdAsync(request.Id);
        if (cls == null)
            throw new NotFoundException("Lớp học", request.Id);

        return ClassMapper.MapToDto(cls);
    }
}
