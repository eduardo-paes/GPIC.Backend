using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.ProjectActivity;
public class UpdateProjectActivityRequest : BaseProjectActivity
{
    [Required]
    public Guid? ProjectId { get; set; }
}