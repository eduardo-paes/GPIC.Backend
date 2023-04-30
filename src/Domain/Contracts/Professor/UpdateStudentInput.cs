namespace Domain.Contracts.Professor
{
    public class UpdateProfessorInput : BaseProfessorContract
    {
        public Guid? Id { get; set; }
    }
}