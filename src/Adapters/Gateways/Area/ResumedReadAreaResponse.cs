using Adapters.Gateways.Base;

namespace Adapters.Gateways.Area
{
    public class ResumedReadAreaResponse : Response
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}