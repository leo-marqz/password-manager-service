using AuthService.Application.DTOs;
using AuthService.Application.Contracts;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Settings;

using System.Threading.Tasks;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace AuthService.Infrastructure.Services
{
    public class ImplAuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public ImplAuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
        }

        public async Task<AuthResultDto> LoginAsync(LoginRequestDto request){
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                 return new AuthResultDto { Success = false, ErrorMessage = "Credenciales inválidas." };
            }

            // Si tiene MFA, aquí deberías pausar la sesión y pedir verificación
            if (user.MfaEnabled)
            {
                return new AuthResultDto
                {
                    Success = false,
                    MfaRequired = true,
                    ErrorMessage = "Se requiere verificación MFA."
                };
            }

            var tokens = GenerateJwtTokens(user);

            return new AuthResultDto
            {
                Success = true,
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            };
        }

        public async Task<AuthResultDto> RegisterAsync(RegisterRequestDto request){
       
            if (await _userRepository.EmailExistsAsync(request.Email))
            {
                return new AuthResultDto { Success = false, ErrorMessage = "El email ya está registrado." };
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                MfaEnabled = false
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync(); // Save changes to the database

            var tokens = GenerateJwtTokens(user); // Generate JWT token for the newly registered user

            return new AuthResultDto
            {
                Success = true,
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            };

        }

        // This method generates a JWT token for the user

        private (string AccessToken, string RefreshToken) GenerateJwtTokens(User user){
            // Validar que el usuario no sea nulo
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null when generating JWT token.");
            }

            // Crear las credenciales de firma del token usando la clave secreta y el algoritmo HMAC SHA256
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear los claims (declaraciones) que se incluirán en el token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("username", user.Username)
            };

            // Crear el token JWT utilizando la configuración proporcionada
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                signingCredentials: credentials
            );

            // Serializar el token a string
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Por ahora el refresh token será un GUID, luego lo almacenaremos en BD
            var refreshToken = Guid.NewGuid().ToString();

            return (accessToken, refreshToken);
        }
    }
}