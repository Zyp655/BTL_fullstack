using MediatR;
using CourseService.DTOs;
using CourseService.Repositories;
using CourseService.Common.Exceptions;

namespace CourseService.Features.Schedules.Queries;

public class GetSchedulesByClassQueryHandler : IRequestHandler<GetSchedulesByClassQuery, List<ScheduleDto>>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IClassRepository _classRepository;

    public GetSchedulesByClassQueryHandler(IScheduleRepository scheduleRepository, IClassRepository classRepository)
    {
        _scheduleRepository = scheduleRepository;
        _classRepository = classRepository;
    }

    public async Task<List<ScheduleDto>> Handle(GetSchedulesByClassQuery request, CancellationToken cancellationToken)
    {
        var classExists = await _classRepository.GetClassByIdAsync(request.ClassId) != null;
        if (!classExists)
            throw new NotFoundException("Lớp học", request.ClassId);

        var schedules = await _scheduleRepository.GetSchedulesByClassAsync(request.ClassId);
        return schedules.Select(ScheduleMapper.MapToDto).ToList();
    }
}
