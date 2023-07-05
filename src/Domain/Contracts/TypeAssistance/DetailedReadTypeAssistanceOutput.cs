namespace Domain.Contracts.TypeAssistance
{
    public class DetailedReadTypeAssistanceOutput : BaseTypeAssistanceContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}