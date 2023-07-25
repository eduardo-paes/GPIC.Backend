using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.ProjectActivity
{
    public abstract class BaseProjectActivityContract
    {
        [Required]
        public Guid? ProjectId { get; set; }
        [Required]
        public Guid? ActivityId { get; set; }
    }
}