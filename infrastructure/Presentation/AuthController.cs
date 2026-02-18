using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shard.DTOs.Auth;

namespace Presentation
{
    [ApiController]
    [Route("api/{controller}")]
    public class AuthController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("Login")] // Url : BaseUrl/api/Auth/Login
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _serviceManager.AuthService.LoginAsync(request);
            return Ok(result);
        }

        [HttpPost("Register")] // Url : BaseUrl/api/Auth/Register
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _serviceManager.AuthService.RegisterASync(request);
            return Ok(result);
        }
    }
}
