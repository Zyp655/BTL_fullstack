using MediatR;
using CourseService.DTOs;
using CourseService.Repositories;

namespace CourseService.Features.Classes.Queries;

public class GetClassesQueryHandler : IRequestHandler<GetClassesQuery, PagedResult<ClassDto>>
{
    private readonly IClassRepository _classRepository;

    public GetClassesQueryHandler(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<PagedResult<ClassDto>> Handle(GetClassesQuery request, CancellationToken cancellationToken)
    {
        var items = await _classRepository.GetClassesAsync(request.CourseId, request.TeacherId, request.Status, request.Search, request.Page, request.PageSize);
        var totalCount = await _classRepository.GetClassesCountAsync(request.CourseId, request.TeacherId, request.Status, request.Search);

        return new PagedResult<ClassDto>
        {
            Items = items.Select(ClassMapper.MapToDto).ToList(),
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
