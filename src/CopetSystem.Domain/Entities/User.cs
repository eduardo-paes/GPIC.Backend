namespace CopetSystem.Domain.Entities
{
    public class User : Entity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? CPF { get; set; }
        public string? Role { get; set; }
    }
}