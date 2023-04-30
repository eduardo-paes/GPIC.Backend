using Domain.Contracts.User;

namespace Domain.Contracts.Professor
{
    public class DetailedReadProfessorOutput : BaseProfessorContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public UserReadOutput? User { get; set; }
    }
}