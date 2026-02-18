using Domain.Entities.Identity;
using Domain.Exceptions.BadRequestEx;
using Domain.Exceptions.NotFoundEx.Identity;
using Domain.Exceptions.UnauthorizedExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions.Auth;
using Shard.DTOs.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                Token = await GenerateTokenAsync(user)
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
                Token = await  GenerateTokenAsync(user)
            };
        }

        

        private async Task<string> GenerateTokenAsync(AppUser user)
        {
            // Token : 
            // 1.Header     : [type, algo]
            // 2.Payload    : [Claims]
            // 3.Signature  : [key]


            // Define claims (the payload data)
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            };

            // Get all roles for Specific user
            var roles = await _userManager.GetRolesAsync(user);

            // add user roles to claims
            foreach(var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // key
            var StringKey = "StrongSecurityKeyStrongSecurityKeyStrongSecurityKeyStrongSecurityKeyStrongSecurityKeyStrongSecurityKey";

            // encrypted key
            // SymmetricSecurityKey : means this key use for Encryption and Decryption 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StringKey));

            // Create Token
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7177",
                audience: "OnlineStore",
                claims: authClaims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            
            // Return Token As a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
