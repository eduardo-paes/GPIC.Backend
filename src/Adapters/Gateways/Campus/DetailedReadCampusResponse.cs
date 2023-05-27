using Adapters.Gateways.Base;

namespace Adapters.Gateways.Campus
{
    public class DetailedReadCampusResponse : Response
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Name { get; set; }
    }
}