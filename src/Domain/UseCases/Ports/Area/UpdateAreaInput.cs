using System.ComponentModel.DataAnnotations;
using Domain.UseCases.Ports.Area;

namespace Domain.Ports.Area
{
    public class UpdateAreaInput : BaseAreaContract
    {
        [Required]
        public Guid? MainAreaId { get; set; }
        public Guid? Id { get; set; }
    }
}