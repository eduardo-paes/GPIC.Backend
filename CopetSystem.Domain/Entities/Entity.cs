using System;
namespace CopetSystem.Domain.Entities
{
	public abstract class Entity
	{
		public Guid? Id { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }

        public void DeactivateEntity() => DeletedAt = DateTime.Now;
        public void ActivateEntity() => DeletedAt = null;
    }
}

