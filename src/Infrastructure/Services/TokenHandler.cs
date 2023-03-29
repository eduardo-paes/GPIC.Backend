using Domain.Interfaces.Services;

namespace Infrastructure.Services
{
    public class TokenHandler : ITokenHandler
    {
        public string GenerateToken(Guid? id, string? role)
        {
            return "Token";
        }
    }
}
