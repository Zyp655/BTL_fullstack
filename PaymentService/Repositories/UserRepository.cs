using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.Models;

namespace PaymentService.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PaymentDbContext _context;

    public UserRepository(PaymentDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<IEnumerable<User>> GetUsersAsync(string? search, string? role, bool? isActive, int page, int pageSize)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(role))
            query = query.Where(u => u.Role == role);

        if (isActive.HasValue)
            query = query.Where(u => u.IsActive == isActive.Value);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(u => u.FullName.Contains(search) 
                                  || u.Username.Contains(search) 
                                  || (u.Email != null && u.Email.Contains(search))
                                  || (u.Specialization != null && u.Specialization.Contains(search))
                                  || (u.Degree != null && u.Degree.Contains(search)));

        return await query
            .OrderByDescending(u => u.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetUsersCountAsync(string? search, string? role, bool? isActive)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(role))
            query = query.Where(u => u.Role == role);

        if (isActive.HasValue)
            query = query.Where(u => u.IsActive == isActive.Value);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(u => u.FullName.Contains(search) 
                                  || u.Username.Contains(search) 
                                  || (u.Email != null && u.Email.Contains(search))
                                  || (u.Specialization != null && u.Specialization.Contains(search))
                                  || (u.Degree != null && u.Degree.Contains(search)));

        return await query.CountAsync();
    }

    public async Task<int> CountUsersAsync(bool activeOnly)
    {
        var query = _context.Users.AsQueryable();
        if (activeOnly)
            query = query.Where(u => u.IsActive);
        return await query.CountAsync();
    }

    public async Task<int> CountUsersByRoleAsync(string role, bool activeOnly)
    {
        var query = _context.Users.Where(u => u.Role == role);
        if (activeOnly)
            query = query.Where(u => u.IsActive);
        return await query.CountAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email != null && u.Email.ToLower() == email.ToLower());
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
