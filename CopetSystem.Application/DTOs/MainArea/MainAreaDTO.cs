using System;
namespace CopetSystem.Application.DTOs.MainArea
{
	public class MainAreaDTO
	{
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

