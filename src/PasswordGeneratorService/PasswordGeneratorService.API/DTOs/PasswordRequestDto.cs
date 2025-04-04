using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordGeneratorService.API.DTOs
{
    public class PasswordRequestDto
    {
        public int Length { get; set; } = 12;
        public bool UseUppercase { get; set; } = true;
        public bool UseLowercase { get; set; } = true;
        public bool UseNumbers { get; set; } = true;
        public bool UseSymbols { get; set; } = true;
        public bool WantsToSave { get; set; } = false;
    }
}