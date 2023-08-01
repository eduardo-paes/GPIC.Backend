using Domain.UseCases.Ports.Activity;

namespace Domain.Ports.Activity;
public class UpdateActivityInput : BaseActivity
{
    public Guid? Id { get; set; }
    public DateTime? DeletedAt { get; set; }
}