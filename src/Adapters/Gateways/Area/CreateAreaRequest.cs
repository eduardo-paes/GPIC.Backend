using Adapters.Gateways.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Area
{
    public class CreateAreaRequest : Request
    {
        [Required]
        public Guid? MainAreaId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}