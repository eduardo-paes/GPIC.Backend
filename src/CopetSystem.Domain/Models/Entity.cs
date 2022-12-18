using System;
namespace CopetSystem.API.Models
{
	public abstract class Entity
	{
		public long Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

