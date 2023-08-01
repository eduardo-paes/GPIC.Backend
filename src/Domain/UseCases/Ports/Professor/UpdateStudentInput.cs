namespace Domain.UseCases.Ports.Professor
{
    public class UpdateProfessorInput : BaseProfessorContract
    {
        public Guid? Id { get; set; }
    }
}