using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Ports.SubArea
{
    public class CreateSubAreaInput : BaseSubAreaContract
    {
        [Required]
        public Guid? AreaId { get; set; }
    }
}