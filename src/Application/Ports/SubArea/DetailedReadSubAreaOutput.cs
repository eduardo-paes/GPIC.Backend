using Application.Ports.Area;

namespace Application.Ports.SubArea
{
    public class DetailedReadSubAreaOutput : BaseSubAreaContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual DetailedReadAreaOutput? Area { get; set; }
    }
}