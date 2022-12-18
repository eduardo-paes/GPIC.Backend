using System;
namespace CopetSystem.API.Models
{
	public class Area : Entity
    {
        public long MainAreaId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}

