using Domain.Contracts.Area;

namespace Domain.Contracts.SubArea
{
    public class DetailedReadSubAreaOutput : BaseSubAreaContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual DetailedReadAreaOutput? Area { get; set; }
    }
}