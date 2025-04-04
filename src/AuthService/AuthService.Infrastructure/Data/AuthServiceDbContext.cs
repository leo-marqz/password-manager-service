using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Data
{
    public class AuthServiceDbContext : DbContext
    {
        public AuthServiceDbContext(DbContextOptions options) : base(options)
        {
        }

         public DbSet<User> Users { set; get; }
        public DbSet<RefreshToken> RefreshTokens {set; get;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Aquí puedes configurar tus entidades, relaciones, etc.

            modelBuilder.Entity<User>(entity=>{
                
                entity.ToTable("Users"); // Nombre de la tabla en la base de datos
                entity.HasKey(u => u.Id); // Llave primaria
                entity.Property(u => u.Email).IsRequired().HasMaxLength(256); // Email requerido y longitud máxima
                entity.Property(u => u.PasswordHash).IsRequired(); // PasswordHash requerido
                entity.Property(u => u.MfaEnabled).IsRequired().HasDefaultValue(false); // MFA habilitado por defecto
            });
        }


    }
}