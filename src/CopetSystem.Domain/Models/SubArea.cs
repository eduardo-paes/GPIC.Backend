using System;
namespace CopetSystem.API.Models
{
	public class SubArea : Entity
	{
        public long AreaId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}

