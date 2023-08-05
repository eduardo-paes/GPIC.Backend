using Application.Ports.User;

namespace Application.Ports.Professor
{
    public class DetailedReadProfessorOutput : BaseProfessorContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public UserReadOutput? User { get; set; }
    }
}