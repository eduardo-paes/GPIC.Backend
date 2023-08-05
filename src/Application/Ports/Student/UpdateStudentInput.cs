namespace Application.Ports.Student
{
    public class UpdateStudentInput : BaseStudentContract
    {
        public Guid? Id { get; set; }
    }
}