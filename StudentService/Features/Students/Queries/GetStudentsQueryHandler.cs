using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;

namespace StudentService.Features.Students.Queries;

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, PagedResult<StudentDto>>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentsQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<PagedResult<StudentDto>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var items = await _studentRepository.GetStudentsAsync(request.Search, request.Gender, request.Page, request.PageSize);
        var totalCount = await _studentRepository.GetStudentsCountAsync(request.Search, request.Gender);

        return new PagedResult<StudentDto>
        {
            Items = items.Select(StudentMapper.MapToDto).ToList(),
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
