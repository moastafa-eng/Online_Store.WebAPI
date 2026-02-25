using Shard.DTOs.Auth;

namespace Services.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<UserResponse> LoginAsync(LoginRequest request);
        Task<UserResponse> RegisterASync(RegisterRequest request);

        Task<bool> CheckEmailExistAsync(string email);
        Task<UserResponse?> GetCurrentUserAsync(string email);
        Task<AddressDto?> GetCurrentUserAddressAsync(string email);
        Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto request);
    }

}
