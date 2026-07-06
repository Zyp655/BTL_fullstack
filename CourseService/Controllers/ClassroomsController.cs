using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseService.Data;
using CourseService.DTOs;
using CourseService.Models;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class ClassroomsController : ControllerBase
{
    private readonly CourseDbContext _context;

    public ClassroomsController(CourseDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Lấy danh sách phòng học và trạng thái hiện tại (Trống, Đang học, Bảo trì)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClassroomDto>>> GetClassrooms()
    {
        var classrooms = await _context.Classrooms.ToListAsync();

        var vnTime = DateTime.UtcNow.AddHours(7);
        int dayVal = vnTime.DayOfWeek switch
        {
            DayOfWeek.Sunday => 0,
            DayOfWeek.Monday => 2,
            DayOfWeek.Tuesday => 3,
            DayOfWeek.Wednesday => 4,
            DayOfWeek.Thursday => 5,
            DayOfWeek.Friday => 6,
            DayOfWeek.Saturday => 7,
            _ => -1
        };
        var timeOfDay = vnTime.TimeOfDay;

        // Lấy danh sách các lớp đang học và có lịch học
        var activeClasses = await _context.Classes
            .Include(c => c.Schedules)
            .Where(c => c.Status == "InProgress")
            .ToListAsync();

        // Lấy danh sách tất cả các lớp đang mở hoặc đang học để lấy danh sách lớp phân công
        var allServiceClasses = await _context.Classes
            .Where(c => c.Status == "InProgress" || c.Status == "Opened")
            .ToListAsync();

        var result = new List<ClassroomDto>();

        foreach (var room in classrooms)
        {
            var dto = new ClassroomDto
            {
                RoomNumber = room.RoomNumber,
                IsMaintenance = room.IsMaintenance,
                Notes = room.Notes
            };

            dto.AssignedClasses = allServiceClasses
                .Where(c => MatchRoom(c.Room, room.RoomNumber))
                .Select(c => c.ClassName)
                .ToList();

            if (room.IsMaintenance)
            {
                dto.Status = "Maintenance";
            }
            else
            {
                // Kiểm tra xem có lớp nào đang sử dụng phòng này tại thời điểm hiện tại không
                var currentClass = activeClasses.FirstOrDefault(c =>
                    MatchRoom(c.Room, room.RoomNumber) &&
                    c.Schedules.Any(s => s.DayOfWeek == dayVal && s.StartTime <= timeOfDay && s.EndTime >= timeOfDay)
                );

                if (currentClass != null)
                {
                    dto.Status = "Occupied";
                    dto.CurrentClassName = currentClass.ClassName;
                }
                else
                {
                    dto.Status = "Vacant";
                }
            }

            result.Add(dto);
        }

        return Ok(result);
    }

    /// <summary>
    /// Lấy danh sách phòng học trống trong khoảng thời gian cụ thể
    /// </summary>
    [HttpGet("available")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ClassroomDto>>> GetAvailableClassrooms([FromQuery] int dayOfWeek, [FromQuery] string startTime, [FromQuery] string endTime, [FromQuery] int? excludeScheduleId = null)
    {
        if (!TimeSpan.TryParse(startTime, out var start) || !TimeSpan.TryParse(endTime, out var end))
        {
            return BadRequest(new { message = "Thời gian không hợp lệ. Vui lòng dùng định dạng HH:mm" });
        }

        var classrooms = await _context.Classrooms
            .Where(r => !r.IsMaintenance)
            .ToListAsync();

        var conflictingSchedules = await _context.Schedules
            .Include(s => s.Class)
            .Where(s => s.DayOfWeek == dayOfWeek &&
                        s.StartTime < end &&
                        s.EndTime > start &&
                        (!excludeScheduleId.HasValue || s.ScheduleId != excludeScheduleId.Value))
            .ToListAsync();

        var occupiedRooms = conflictingSchedules
            .Select(s => !string.IsNullOrEmpty(s.Room) ? s.Room : s.Class.Room)
            .Where(r => !string.IsNullOrEmpty(r))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var result = classrooms
            .Where(r => !occupiedRooms.Any(or => MatchRoom(or, r.RoomNumber)))
            .Select(r => new ClassroomDto
            {
                RoomNumber = r.RoomNumber,
                IsMaintenance = r.IsMaintenance,
                Notes = r.Notes,
                Status = "Vacant"
            })
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Tạo phòng học mới (Admin)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ClassroomDto>> CreateClassroom(CreateClassroomDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.RoomNumber))
            return BadRequest(new { message = "Số phòng không được để trống" });

        var normalizedRoom = dto.RoomNumber.Trim();
        if (normalizedRoom.StartsWith("P.", StringComparison.OrdinalIgnoreCase))
        {
            normalizedRoom = normalizedRoom.Substring(2);
        }
        else if (normalizedRoom.StartsWith("Phòng ", StringComparison.OrdinalIgnoreCase))
        {
            normalizedRoom = normalizedRoom.Substring(6);
        }

        var existing = await _context.Classrooms.FindAsync(normalizedRoom);
        if (existing != null)
            return BadRequest(new { message = $"Phòng học {normalizedRoom} đã tồn tại" });

        var room = new Classroom
        {
            RoomNumber = normalizedRoom,
            IsMaintenance = false,
            Notes = dto.Notes
        };

        _context.Classrooms.Add(room);
        await _context.SaveChangesAsync();

        return Ok(new ClassroomDto
        {
            RoomNumber = room.RoomNumber,
            IsMaintenance = room.IsMaintenance,
            Notes = room.Notes,
            Status = "Vacant",
            AssignedClasses = new List<string>()
        });
    }

    /// <summary>
    /// Cập nhật trạng thái bảo trì của phòng học (Admin)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("{roomNumber}/maintenance")]
    public async Task<ActionResult<ClassroomDto>> UpdateMaintenance(string roomNumber, UpdateClassroomMaintenanceDto dto)
    {
        var room = await _context.Classrooms.FindAsync(roomNumber);
        if (room == null)
            return NotFound(new { message = $"Không tìm thấy phòng học {roomNumber}" });

        room.IsMaintenance = dto.IsMaintenance;
        room.Notes = dto.Notes;

        await _context.SaveChangesAsync();

        // Tính toán lại trạng thái phòng để trả về
        var vnTime = DateTime.UtcNow.AddHours(7);
        int dayVal = vnTime.DayOfWeek switch
        {
            DayOfWeek.Sunday => 0,
            DayOfWeek.Monday => 2,
            DayOfWeek.Tuesday => 3,
            DayOfWeek.Wednesday => 4,
            DayOfWeek.Thursday => 5,
            DayOfWeek.Friday => 6,
            DayOfWeek.Saturday => 7,
            _ => -1
        };
        var timeOfDay = vnTime.TimeOfDay;

        var activeClasses = await _context.Classes
            .Include(c => c.Schedules)
            .Where(c => c.Status == "InProgress")
            .ToListAsync();

        var allServiceClasses = await _context.Classes
            .Where(c => c.Status == "InProgress" || c.Status == "Opened")
            .ToListAsync();

        var resultDto = new ClassroomDto
        {
            RoomNumber = room.RoomNumber,
            IsMaintenance = room.IsMaintenance,
            Notes = room.Notes
        };

        resultDto.AssignedClasses = allServiceClasses
            .Where(c => MatchRoom(c.Room, room.RoomNumber))
            .Select(c => c.ClassName)
            .ToList();

        if (room.IsMaintenance)
        {
            resultDto.Status = "Maintenance";
        }
        else
        {
            var currentClass = activeClasses.FirstOrDefault(c =>
                MatchRoom(c.Room, room.RoomNumber) &&
                c.Schedules.Any(s => s.DayOfWeek == dayVal && s.StartTime <= timeOfDay && s.EndTime >= timeOfDay)
            );

            if (currentClass != null)
            {
                resultDto.Status = "Occupied";
                resultDto.CurrentClassName = currentClass.ClassName;
            }
            else
            {
                resultDto.Status = "Vacant";
            }
        }

        return Ok(resultDto);
    }

    private static bool MatchRoom(string? classRoom, string roomNum)
    {
        if (string.IsNullOrWhiteSpace(classRoom)) return false;
        
        // Hỗ trợ so khớp: "301" với "301", "P.301", "Phòng 301"
        return classRoom.Equals(roomNum, StringComparison.OrdinalIgnoreCase) ||
               classRoom.Equals($"P.{roomNum}", StringComparison.OrdinalIgnoreCase) ||
               classRoom.Equals($"Phòng {roomNum}", StringComparison.OrdinalIgnoreCase);
    }
}
