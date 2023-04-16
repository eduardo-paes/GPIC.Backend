namespace Domain.Contracts.Student
{
    public class UpdateStudentInput : BaseStudentContract
    {
        public Guid? Id { get; set; }
    }
}