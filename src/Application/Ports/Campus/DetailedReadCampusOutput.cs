namespace Application.Ports.Campus
{
    public class DetailedReadCampusOutput : BaseCampusContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}