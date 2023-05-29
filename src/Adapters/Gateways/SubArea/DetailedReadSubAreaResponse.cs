using Adapters.Gateways.Area;
using Adapters.Gateways.Base;

namespace Adapters.Gateways.SubArea
{
    public class DetailedReadSubAreaResponse : IResponse
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual DetailedReadAreaResponse? Area { get; set; }
    }
}