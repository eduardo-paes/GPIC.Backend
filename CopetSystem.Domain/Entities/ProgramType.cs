using System;
using CopetSystem.Domain.Entities.Primitives;

namespace CopetSystem.Domain.Entities
{
	public class ProgramType : Entity
    {
		public string? Name { get; private set; }
		public string? Description { get; private set; }
    }
}

