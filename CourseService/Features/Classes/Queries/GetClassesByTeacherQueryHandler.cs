using MediatR;
using CourseService.DTOs;
using CourseService.Repositories;

namespace CourseService.Features.Classes.Queries;

public class GetClassesByTeacherQueryHandler : IRequestHandler<GetClassesByTeacherQuery, List<ClassDto>>
{
    private readonly IClassRepository _classRepository;

    public GetClassesByTeacherQueryHandler(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<List<ClassDto>> Handle(GetClassesByTeacherQuery request, CancellationToken cancellationToken)
    {
        var classes = await _classRepository.GetClassesByTeacherAsync(request.TeacherId);
        return classes.Select(ClassMapper.MapToDto).ToList();
    }
}
