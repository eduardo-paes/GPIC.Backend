using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.ProjectActivity;
public class UpdateProjectActivityInput : BaseProjectActivityContract
{
    [Required]
    public Guid? ProjectId { get; set; }
}