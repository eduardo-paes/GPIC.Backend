using System.ComponentModel.DataAnnotations;

namespace CopetSystem.Application.DTOs.SubArea
{
    public class CreateSubAreaDTO : BaseSubAreaDTO
    {
        [Required]
        public Guid? AreaId { get; set; }
    }
}

