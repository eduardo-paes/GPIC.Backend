using Adapters.Gateways.Base;

namespace Adapters.Gateways.ProgramType
{
    public class DetailedReadProgramTypeResponse : Response
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}