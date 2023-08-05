using System.ComponentModel.DataAnnotations;

namespace Application.Ports.Area
{
    public class CreateAreaInput : BaseAreaContract
    {
        [Required]
        public Guid? MainAreaId { get; set; }
    }
}