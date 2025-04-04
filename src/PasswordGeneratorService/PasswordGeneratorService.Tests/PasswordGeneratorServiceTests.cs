using PasswordGeneratorService.API.DTOs;
using PasswordGeneratorService.API.Services;

namespace PasswordGeneratorService.Tests;

public class PasswordGeneratorServiceTests
{
    private readonly ImplPasswordGeneratorService _passwordGeneratorService;

    public PasswordGeneratorServiceTests()
    {
        _passwordGeneratorService = new ImplPasswordGeneratorService();
    }

    [Fact]
    public void GeneratePassword_ShouldReturnPassword_WhenCalled()
    {
        var request = new PasswordRequestDto
        {
            Length = 12, // Example length
            UseUppercase = true,
            UseLowercase = true,
            UseNumbers = true,
            UseSymbols = true,
            WantsToSave = false // Set to false for this test case
        };

        // Act
        var result = _passwordGeneratorService.Generate(request, false);

        // Assert
        Assert.NotNull(result); // Ensure the result is not null
        Assert.False(string.IsNullOrEmpty(result.Password)); // Ensure the password is not empty or null
        Assert.Equal(12, result.Password.Length); // Ensure the password length matches the requested length
    }
}