using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Ports.ProjectActivity
{
    public abstract class BaseProjectActivityContract
    {
        [Required]
        public Guid? ActivityId { get; set; }
        [Required]
        public int? InformedActivities { get; set; }
    }
}