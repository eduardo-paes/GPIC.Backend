using System;
namespace CopetSystem.Domain.Entities
{
	public class Area : Entity
    {
        public long MainAreaId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}

