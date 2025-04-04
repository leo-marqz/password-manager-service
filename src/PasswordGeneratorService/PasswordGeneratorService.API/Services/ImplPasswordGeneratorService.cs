using System.Text;
using PasswordGeneratorService.API.DTOs;

namespace PasswordGeneratorService.API.Services
{
    public class ImplPasswordGeneratorService : IPasswordGeneratorService
    {
        const string UPPERCASE_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string LOWERCASE_CHARS = "abcdefghijklmnopqrstuvwxyz";
        const string NUMBER_CHARS = "0123456789";
        const string SYMBOL_CHARS = "!@#$%^&*()_-+=<>?";

        public PasswordResponseDto Generate(PasswordRequestDto request, bool isAuthenticated)
        {
            var charPool = new StringBuilder();
            if(request.UseUppercase) charPool.Append(UPPERCASE_CHARS);
            if(request.UseLowercase) charPool.Append(LOWERCASE_CHARS);
            if(request.UseNumbers) charPool.Append(NUMBER_CHARS);
            if(request.UseSymbols) charPool.Append(SYMBOL_CHARS);

            if(charPool.Length == 0){
                // If no character types are selected, return an error message
                return new PasswordResponseDto
                {
                    Password = string.Empty,
                    Message = "At least one character type must be selected."
                };
            }

            var random = new Random();
            var result = new string(
                Enumerable.Repeat(charPool.ToString(), request.Length)
                .Select(c => c[random.Next(c.Length)]) // Select a random character from the pool for each position in the password
                .ToArray()
                );

            if(request.WantsToSave && isAuthenticated){
                // Logic to save the password to a database or a secure store can be implemented here
            }

            // Return the generated password and a success message
            var message = request.WantsToSave && !isAuthenticated 
                ? "Password generated successfully but cannot be saved as the user is not authenticated."
                : "Password generated successfully.";

            return new PasswordResponseDto {
                Password = result,
                Message = message
             };
                                    
        }
    }
}