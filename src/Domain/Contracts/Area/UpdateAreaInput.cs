using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Area
{
    public class UpdateAreaInput : BaseAreaContract
    {
        [Required]
        public Guid? MainAreaId { get; set; }
        public Guid? Id { get; set; }
    }
}