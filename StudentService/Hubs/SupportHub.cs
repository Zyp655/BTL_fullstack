using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using System;
using System.Threading.Tasks;

namespace StudentService.Hubs;

public class SupportHub : Hub
{
    private readonly StudentDbContext _context;

    public SupportHub(StudentDbContext context)
    {
        _context = context;
    }

    public override async Task OnConnectedAsync()
    {
        // Check if user is in role Admin
        if (Context.User?.IsInRole("Admin") == true)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
        }

        // Check if user is HocVien (Student)
        if (Context.User?.IsInRole("HocVien") == true)
        {
            var userIdStr = Context.User.FindFirst("userId")?.Value;
            if (int.TryParse(userIdStr, out int userId))
            {
                var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.UserId == userId);
                if (student != null)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, $"Student_{student.StudentId}");
                }
            }
        }

        await base.OnConnectedAsync();
    }
}
