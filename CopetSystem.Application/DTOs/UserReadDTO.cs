using System;
namespace CopetSystem.Application.DTOs
{
	public class UserReadDTO
	{
		public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? CPF { get; set; }
        public string? Role { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

