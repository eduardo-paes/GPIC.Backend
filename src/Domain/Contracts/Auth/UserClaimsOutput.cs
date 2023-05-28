namespace Domain.Contracts.Auth
{
    public class UserClaimsOutput
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
    }
}