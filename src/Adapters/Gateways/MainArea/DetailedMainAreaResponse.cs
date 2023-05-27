using Adapters.Gateways.Base;

namespace Adapters.Gateways.MainArea
{
    public class DetailedMainAreaResponse : Response
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}