using Adapters.DTOs.Base;

namespace Adapters.DTOs.Auth
{
    public class UserLoginResponseDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}