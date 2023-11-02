using System.ComponentModel.DataAnnotations;

namespace Application.Ports.SubArea
{
    public class CreateSubAreaInput : BaseSubAreaContract
    {
        [Required]
        public Guid? AreaId { get; set; }
    }
}