using Shard.DTOs.Auth;

namespace Services.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<UserResponse> LoginAsync(LoginRequest request);
        Task<UserResponse> RegisterASync(RegisterRequest request);
    }

}
