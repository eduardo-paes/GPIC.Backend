using System;
using Domain.Entities.Primitives;

namespace Domain.Entities
{
    public class ProgramType : Entity
    {
        public string? Name { get; private set; }
        public string? Description { get; private set; }
    }
}

