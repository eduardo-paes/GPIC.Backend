using Domain.UseCases.Ports.User;

namespace Domain.UseCases.Ports.Professor
{
    public class DetailedReadProfessorOutput : BaseProfessorContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public UserReadOutput? User { get; set; }
    }
}