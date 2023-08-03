using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Ports.ProjectActivity
{
    public class UpdateProjectActivityInput : BaseProjectActivityContract
    {
        [Required]
        public Guid? ProjectId { get; set; }
    }
}