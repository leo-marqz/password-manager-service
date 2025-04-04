using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Application.DTOs
{
    public class AuthResultDto
    {
        public bool Success { get; set; } = false;
        public string AccessToken { get; set; } = string.Empty; 
        public string RefreshToken { get; set; } = string.Empty; 
        public string ErrorMessage { get; set; } = string.Empty; 
        public bool MfaRequired { get; set; } = false;
    }
}