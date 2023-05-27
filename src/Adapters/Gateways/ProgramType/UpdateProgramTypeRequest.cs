using Adapters.Gateways.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.ProgramType
{
    public class UpdateProgramTypeRequest : Request
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? Id { get; set; }
    }
}