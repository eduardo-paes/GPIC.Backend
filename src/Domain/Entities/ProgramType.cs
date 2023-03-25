using Domain.Entities.Primitives;

namespace Domain.Entities
{
    /// <summary>
    /// Tipo de Programa
    /// </summary>
    public class ProgramType : Entity
    {
        public string? Name { get; }
        public string? Description { get; }

        public ProgramType(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public ProgramType() { }
    }
}