
using AuthService.Domain.Entities;

namespace AuthService.Application.Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task<bool> EmailExistsAsync(string email);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
    }
}