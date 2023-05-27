using Adapters.Gateways.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.SubArea
{
    public class CreateSubAreaRequest : Request
    {
        [Required]
        public Guid? AreaId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}

