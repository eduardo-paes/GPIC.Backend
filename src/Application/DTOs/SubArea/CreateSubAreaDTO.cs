using Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.SubArea
{
    public class CreateSubAreaDTO : RequestDTO
    {
        [Required]
        public Guid? AreaId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}

