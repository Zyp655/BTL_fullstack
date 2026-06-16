using MediatR;
using CourseService.DTOs;
using CourseService.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace CourseService.Features.Courses.Queries;

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, PagedResult<CourseDto>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMemoryCache _memoryCache;

    public GetCoursesQueryHandler(ICourseRepository courseRepository, IMemoryCache memoryCache)
    {
        _courseRepository = courseRepository;
        _memoryCache = memoryCache;
    }

    public async Task<PagedResult<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        // Get the current cache version for courses (default to 1)
        var version = _memoryCache.GetOrCreate("courses_version", entry => 1);
        
        var cacheKey = $"courses_v{version}_{request.Search}_{request.Category}_{request.Level}_{request.IsActive}_{request.Page}_{request.PageSize}";

        if (!_memoryCache.TryGetValue(cacheKey, out PagedResult<CourseDto>? cachedResult))
        {
            var items = await _courseRepository.GetCoursesAsync(request.Search, request.Category, request.Level, request.IsActive, request.Page, request.PageSize);
            var totalCount = await _courseRepository.GetCoursesCountAsync(request.Search, request.Category, request.Level, request.IsActive);

            var courseDtos = items.Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                Level = c.Level,
                Category = c.Category,
                Fee = c.Fee,
                TotalSessions = c.TotalSessions,
                DurationWeeks = c.DurationWeeks,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                ClassCount = c.Classes?.Count ?? 0
            }).ToList();

            cachedResult = new PagedResult<CourseDto>
            {
                Items = courseDtos,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize
            };

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));

            _memoryCache.Set(cacheKey, cachedResult, cacheEntryOptions);
        }

        return cachedResult!;
    }
}
