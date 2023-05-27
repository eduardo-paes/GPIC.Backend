using Adapters.Gateways.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.SubArea
{
    public class UpdateSubAreaRequest : Request
    {
        [Required]
        public Guid? AreaId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
        public Guid? Id { get; set; }
    }
}

