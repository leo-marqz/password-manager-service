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
                entity.HasKey(user => user.Id); // Llave primaria
                entity.Property(user=> user.Username).HasMaxLength(50).IsRequired(); 
                entity.Property(user=> user.Email).IsRequired();
                entity.Property(user=> user.PasswordHash).IsRequired(); 
                entity.HasMany(user => user.RefreshTokens).WithOne(rt => rt.User).HasForeignKey(rt => rt.UserId);
            });

            modelBuilder.Entity<RefreshToken>(entity => 
            {
                entity.HasKey(rt => rt.Id);
                entity.Property(rt => rt.Token).IsRequired(); 
                entity.HasIndex(rt => rt.Token).IsUnique(); 
                entity.Property(rt => rt.ExpiresAt).IsRequired();
                entity.Property(rt => rt.CreatedAt).IsRequired();
            });
        }


    }
}