using Adapters.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.DTOs.Area
{
    public class CreateAreaDTO : RequestDTO
    {
        [Required]
        public Guid? MainAreaId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}