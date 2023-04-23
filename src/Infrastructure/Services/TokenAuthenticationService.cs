using Domain.Interfaces.Services;

namespace Infrastructure.Services;
public class TokenAuthenticationService : ITokenAuthenticationService
{
    public string GenerateToken(Guid? id, string? role)
    {
        return "Token";
    }
}
