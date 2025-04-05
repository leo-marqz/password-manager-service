
using AuthService.Application.Contracts;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthServiceDbContext _context;

        public UserRepository(AuthServiceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await _context.Users.AddAsync(user); 
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email);
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }

            return _context.Users
                .AsNoTracking() // Mejora el rendimiento para consultas de solo lectura
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty.", nameof(id));
            }

            return _context.Users
                .AsNoTracking() // Mejora el rendimiento para consultas de solo lectura
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public Task SaveChangesAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones según sea necesario
                throw new Exception("Error al guardar cambios en la base de datos.", ex);
            }
        }
    }
}