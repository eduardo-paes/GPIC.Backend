using Adapters.Gateways.Base;

namespace Adapters.Gateways.Auth
{
    public class UserLoginResponse : Response
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}