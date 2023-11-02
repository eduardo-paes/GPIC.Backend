namespace Application.Ports.User
{
    public class UserReadOutput
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public int? Role { get; set; }
        public string? Email { get; set; }
        public string? CPF { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}