using System.ComponentModel.DataAnnotations;

namespace Application.Ports.ProjectActivity
{
    public class UpdateProjectActivityInput : BaseProjectActivityContract
    {
        [Required]
        public Guid? ProjectId { get; set; }
    }
}