namespace Domain.Interfaces
{
    public interface ITokenHandler
    {
        string GenerateToken(Guid? id, string? role);
    }
}