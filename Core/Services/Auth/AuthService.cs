using AutoMapper;
using Domain.Entities.Identity;
using Domain.Exceptions.BadRequestEx;
using Domain.Exceptions.NotFoundEx.Identity;
using Domain.Exceptions.UnauthorizedExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions.Auth;
using Shard.DTOs.Auth;
using Shard.JWT;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Auth
{
    // IOption => Options Design Pattern
    public class AuthService(UserManager<AppUser> _userManager, IOptions<JwtOptions> _options, IMapper _mapper) : IAuthService
    {
        public async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task<AddressDto?> GetCurrentUserAddressAsync(string email)
        {
            // this method dose not load the Navigational Properties
            //var currentUser = await _userManager.FindByEmailAsync(email);


            var currentUser = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower());
            if (currentUser is null) throw new UserNotFoundException(currentUser.Id);

            return _mapper.Map<AddressDto>(currentUser.Address);
        }

        public async Task<UserResponse?> GetCurrentUserAsync(string email)
        {
            var currentUser = await _userManager.FindByEmailAsync(email);

            if (currentUser is null) throw new UserNotFoundException(currentUser.Id);

            return new UserResponse 
            {
                Email = currentUser.Email,
                DisplayName = currentUser.DisplayName,
                Token = await GenerateTokenAsync(currentUser)
            };
        }
        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto request)
        {
            var currentUser =await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower());
            if (currentUser is null) throw new UserNotFoundException(currentUser.Id);


            if(currentUser.Address is null)
            {
                // Create new address

                currentUser.Address = _mapper.Map<Address>(request);
            }

            // update the old address

            currentUser.Address.FirstName = request.FirstName;
            currentUser.Address.LastName = request.LastName;
            currentUser.Address.City = request.City;
            currentUser.Address.Street = request.Street;
            currentUser.Address.Country = request.Country;

            await _userManager.UpdateAsync(currentUser);

            return _mapper.Map<AddressDto>(currentUser.Address);
            
        }

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

            var options = _options.Value; // Obj from _options

            // encrypted key
            // SymmetricSecurityKey : means this key use for Encryption and Decryption 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecurityKey));

            // Create Token
            var token = new JwtSecurityToken(
                issuer: options.Issure,
                audience: options.Audience,
                claims: authClaims,
                expires: DateTime.UtcNow.AddDays(double.Parse(options.DurationInDayes)),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            
            // Return Token As a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
