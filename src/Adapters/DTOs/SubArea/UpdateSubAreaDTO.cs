using Adapters.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.DTOs.SubArea
{
    public class UpdateSubAreaDTO : RequestDTO
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

