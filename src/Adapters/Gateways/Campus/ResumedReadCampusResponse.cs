using Adapters.Gateways.Base;

namespace Adapters.Gateways.Campus
{
    public class ResumedReadCampusResponse : Response
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
    }
}