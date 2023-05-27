using Adapters.Gateways.Base;

namespace Adapters.Gateways.StudentAssistanceScholarship
{
    public class ResumedReadStudentAssistanceScholarshipResponse : Response
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}