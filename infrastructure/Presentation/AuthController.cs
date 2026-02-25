using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shard.DTOs.Auth;
using System.Security.Claims;

namespace Presentation
{
    [ApiController]
    [Route("api/{controller}")]
    public class AuthController(IServiceManager _serviceManager) : ControllerBase
    {

        // Login
        [HttpPost("Login")] // Url : BaseUrl/api/Auth/Login
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _serviceManager.AuthService.LoginAsync(request);
            return Ok(result);
        }


        // Register
        [HttpPost("Register")] // Url : BaseUrl/api/Auth/Register
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _serviceManager.AuthService.RegisterASync(request);
            return Ok(result);
        }


        // check Email Exist 
        [HttpGet("checkemail/{email}")] // Url : BaseUrl/api/Auth/checkemail
        public async Task<IActionResult> CheckEmailExist(string email)
        {
            var result = await _serviceManager.AuthService.CheckEmailExistAsync(email);

            return Ok(result);
        }



        // Get Current User
        [HttpGet("getuser")] // Url : BaseUrl/api/Auth/getuser
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthService.GetCurrentUserAsync(userEmail!.Value);

            return Ok(result);
        }


        // Get Current User Address
        [HttpGet("getuseraddress")] // Url : BaseUrl/api/Auth/getuseraddress
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthService.GetCurrentUserAddressAsync(userEmail!.Value);

            return Ok(result);
        }



        // Update Current User Address
        [HttpPut("updateuseraddress")] // Url: BaseUrl/api/Auth/updateuseraddress
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto request)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);

            var result = await _serviceManager.AuthService.UpdateCurrentUserAddressAsync(userEmail!.Value, request);

            return Ok(result);
        }
    }
}
