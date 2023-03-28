using Adapters.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.DTOs.Area
{
    public class UpdateAreaDTO : RequestDTO
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

