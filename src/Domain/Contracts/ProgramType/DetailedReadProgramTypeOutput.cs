namespace Domain.Contracts.ProgramType
{
    public class DetailedReadProgramTypeOutput : BaseProgramTypeContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}