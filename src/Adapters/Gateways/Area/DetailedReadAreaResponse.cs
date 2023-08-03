using Adapters.Gateways.Base;
using Adapters.Gateways.MainArea;

namespace Adapters.Gateways.Area
{
    public class DetailedReadAreaResponse : IResponse
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual DetailedReadMainAreaResponse? MainArea { get; set; }
    }
}