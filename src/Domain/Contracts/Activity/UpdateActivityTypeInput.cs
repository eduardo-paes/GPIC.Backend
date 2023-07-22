namespace Domain.Contracts.Activity;
public class UpdateActivityTypeInput : BaseActivityType
{
    public Guid? Id { get; set; }
    public DateTime? DeletedAt { get; set; }
    new public IList<UpdateActivityInput>? Activities { get; set; }
}