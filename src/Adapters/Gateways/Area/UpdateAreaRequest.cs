using Adapters.Gateways.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Area
{
    public class UpdateAreaRequest : Request
    {
        public Guid? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public Guid? MainAreaId { get; set; }
    }
}