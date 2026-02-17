using Domain.Entities.Identity;
using Domain.Exceptions.BadRequestEx;
using Domain.Exceptions.NotFoundEx.Identity;
using Domain.Exceptions.UnauthorizedExceptions;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions.Auth;
using Shard.DTOs.Auth;

namespace Services.Auth
{
    public class AuthService(UserManager<AppUser> _userManager) : IAuthService
    {
        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            // Find user by Email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundException(request.Email);

            // Check Password
            var flag = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!flag) throw new UnauthorizedException();

            return new UserResponse()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = "Token will be here"
            };
        }

        public async Task<UserResponse> RegisterASync(RegisterRequest request)
        {
            var user = new AppUser()
            {
                Email = request.Email,
                DisplayName = request.DisplayName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new RegistrationBadRequestException(result.Errors.Select(e => e.Description).ToList());

            return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "Token will be here"
            };
        }
    }
}
