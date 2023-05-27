using System.ComponentModel.DataAnnotations;
using Adapters.Gateways.Base;

namespace Adapters.Gateways.Campus
{
    public class CreateCampusRequest : Request
    {
        [Required]
        public string? Name { get; set; }
    }
}