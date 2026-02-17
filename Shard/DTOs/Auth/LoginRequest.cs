using System.ComponentModel.DataAnnotations;

namespace Shard.DTOs.Auth
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
