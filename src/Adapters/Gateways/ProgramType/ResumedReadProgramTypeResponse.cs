using Adapters.Gateways.Base;

namespace Adapters.Gateways.ProgramType
{
    public class ResumedReadProgramTypeResponse : Response
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}