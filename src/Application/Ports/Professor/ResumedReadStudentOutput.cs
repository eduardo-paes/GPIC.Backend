namespace Application.Ports.Professor
{
    public class ResumedReadProfessorOutput : BaseProfessorContract
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}