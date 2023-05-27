using Adapters.Gateways.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.MainArea
{
    public class CreateMainAreaRequest : Request
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}