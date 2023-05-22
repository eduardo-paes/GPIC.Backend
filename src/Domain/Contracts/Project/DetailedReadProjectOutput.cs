namespace Domain.Contracts.Project
{
    public class DetailedReadProjectOutput : BaseProjectContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}