using Adapters.Gateways.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.MainArea
{
    public class UpdateMainAreaRequest : Request
    {
        public Guid? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}