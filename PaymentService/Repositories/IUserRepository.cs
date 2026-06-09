using PaymentService.Models;

namespace PaymentService.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<IEnumerable<User>> GetUsersAsync(string? search, string? role, bool? isActive, int page, int pageSize);
    Task<int> GetUsersCountAsync(string? search, string? role, bool? isActive);
    Task<int> CountUsersAsync(bool activeOnly);
    Task<int> CountUsersByRoleAsync(string role, bool activeOnly);
    Task AddUserAsync(User user);
    void UpdateUser(User user);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<bool> SaveChangesAsync();
}
