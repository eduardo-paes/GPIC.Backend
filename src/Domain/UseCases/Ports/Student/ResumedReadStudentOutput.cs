namespace Domain.UseCases.Ports.Student
{
    public class ResumedReadStudentOutput : BaseStudentContract
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}