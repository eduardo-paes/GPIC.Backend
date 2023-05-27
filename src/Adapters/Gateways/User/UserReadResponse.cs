using Adapters.Gateways.Base;

namespace Adapters.Gateways.User
{
    public class UserReadResponse : Response
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? CPF { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}