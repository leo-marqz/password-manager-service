using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class RefreshToken
    {
         public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relación inversa (foreign key)
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
    }
}