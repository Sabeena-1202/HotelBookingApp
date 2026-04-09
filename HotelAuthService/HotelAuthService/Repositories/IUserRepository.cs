using HotelAuthService.Models;

namespace HotelAuthService.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int userId);
        Task<bool> EmailExistsAsync(string email);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
    }
}