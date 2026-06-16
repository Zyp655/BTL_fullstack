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

        var result = new List<ClassroomDto>();

        foreach (var room in classrooms)
        {
            var dto = new ClassroomDto
            {
                RoomNumber = room.RoomNumber,
                IsMaintenance = room.IsMaintenance,
                Notes = room.Notes
            };

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

        var resultDto = new ClassroomDto
        {
            RoomNumber = room.RoomNumber,
            IsMaintenance = room.IsMaintenance,
            Notes = room.Notes
        };

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
