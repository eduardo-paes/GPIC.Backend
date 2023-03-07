using System.ComponentModel.DataAnnotations;

namespace CopetSystem.Application.DTOs.SubArea
{
    public class UpdateSubAreaDTO : BaseSubAreaDTO
    {
        [Required]
        public Guid? AreaId { get; set; }
        public Guid? Id { get; set; }
    }
}

