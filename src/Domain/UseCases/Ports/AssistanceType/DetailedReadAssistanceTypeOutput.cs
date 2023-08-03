namespace Domain.UseCases.Ports.AssistanceType
{
    public class DetailedReadAssistanceTypeOutput : BaseAssistanceTypeContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}