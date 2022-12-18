using System;
namespace CopetSystem.Domain.Entities
{
	public abstract class Entity
	{
		public long Id { get; protected set; }
        public DateTime? DeletedAt { get; set; }
    }
}

