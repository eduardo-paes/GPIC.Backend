using Adapters.DTOs.Base;

namespace Adapters.DTOs.Auth
{
    public class UserLoginResponseDTO : ResponseDTO
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}