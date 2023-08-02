using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Ports.Area
{
    public class UpdateAreaInput : BaseAreaContract
    {
        [Required]
        public Guid? MainAreaId { get; set; }
        public Guid? Id { get; set; }
    }
}