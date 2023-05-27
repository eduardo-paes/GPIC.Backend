using System.ComponentModel.DataAnnotations;
using Adapters.Gateways.Base;

namespace Adapters.Gateways.ProgramType
{
    public class CreateProgramTypeRequest : Request
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}