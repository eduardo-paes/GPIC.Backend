namespace Domain.Interfaces.Services
{
    public interface ITokenHandler
    {
        string GenerateToken(Guid? id, string? role);
    }
}