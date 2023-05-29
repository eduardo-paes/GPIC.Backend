using Adapters.Gateways.MainArea;
using Adapters.Gateways.Base;

namespace Adapters.Gateways.Area
{
    public class DetailedReadAreaResponse : IResponse
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual DetailedMainAreaResponse? MainArea { get; set; }
    }
}