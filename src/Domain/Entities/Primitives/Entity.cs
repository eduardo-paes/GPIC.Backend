﻿namespace Domain.Entities.Primitives
{
    public abstract class Entity
    {
        public Guid? Id { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }

        public void DeactivateEntity() => DeletedAt = DateTime.UtcNow;
        public void ActivateEntity() => DeletedAt = null;
    }
}