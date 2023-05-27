using Adapters.Gateways.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Campus
{
    public class UpdateCampusRequest : Request
    {
        [Required]
        public string? Name { get; set; }
        public Guid? Id { get; set; }
    }
}