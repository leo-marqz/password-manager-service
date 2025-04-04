
using PasswordGeneratorService.API.DTOs;

namespace PasswordGeneratorService.API.Services
{
    public interface IPasswordGeneratorService
    {
        PasswordResponseDto Generate(PasswordRequestDto request, bool isAuthenticated);
    }
}