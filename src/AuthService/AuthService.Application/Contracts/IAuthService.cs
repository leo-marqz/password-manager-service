
using AuthService.Application.DTOs;

namespace AuthService.Application.Contracts
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResultDto> LoginAsync(LoginRequestDto request);
    }
}