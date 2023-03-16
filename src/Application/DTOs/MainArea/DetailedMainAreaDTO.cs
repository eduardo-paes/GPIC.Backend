namespace Application.DTOs.MainArea
{
    public class DetailedMainAreaDTO : BaseMainAreaDTO
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}