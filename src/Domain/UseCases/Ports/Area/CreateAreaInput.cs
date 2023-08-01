using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Ports.Area
{
    public class CreateAreaInput : BaseAreaContract
    {
        [Required]
        public Guid? MainAreaId { get; set; }
    }
}