using Microsoft.AspNetCore.Mvc;
using PasswordGeneratorService.API.DTOs;
using PasswordGeneratorService.API.Services;


namespace PasswordGeneratorService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordGeneratorService _passwordGeneratorService;

        public PasswordController(IPasswordGeneratorService passwordGeneratorService)
        {
            _passwordGeneratorService = passwordGeneratorService ?? throw new ArgumentNullException(nameof(passwordGeneratorService));
        }

        [HttpPost("generate")]
        public IActionResult Generate([FromBody] PasswordRequestDto request){
            if(request == null)
            {
                return BadRequest("Invalid password request.");
            }

            // Simulate user authentication status, in real-world this would come from the auth context
            //-----?
            bool isAuthenticated = false;

            var response = _passwordGeneratorService.Generate(request, isAuthenticated);

            if(string.IsNullOrEmpty(response.Password))
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
    }
}