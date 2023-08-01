namespace Domain.UseCases.Ports.ProgramType
{
    public class UpdateProgramTypeInput : BaseProgramTypeContract
    {
        public Guid? Id { get; set; }
    }
}