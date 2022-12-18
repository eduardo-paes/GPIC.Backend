using System;
namespace CopetSystem.Domain.Entities
{
	public class SubArea : Entity
	{
        public long AreaId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}

