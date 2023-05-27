using Adapters.Gateways.Base;

namespace Adapters.Gateways.SubArea
{
    public class ResumedReadSubAreaResponse : Response
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
