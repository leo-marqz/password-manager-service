using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty; // This should be a long, random string stored securely (e.g., in environment variables or a secure vault)
        public string Issuer { get; set; } = "AuthService"; // This should match the issuer in your token validation settings
        public string Audience { get; set; } = "PasswordManagerAPI"; // This should match the audience in your token validation settings
        public int AccessTokenExpirationMinutes { get; set; } = 30; // Access token expiration time in minutes
        public int RefreshTokenExpirationDays { get; set; } = 7; // Refresh token expiration time in days
    }
}