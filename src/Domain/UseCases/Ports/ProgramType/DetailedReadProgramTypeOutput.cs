namespace Domain.UseCases.Ports.ProgramType
{
    public class DetailedReadProgramTypeOutput : BaseProgramTypeContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}