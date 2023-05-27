using System.ComponentModel.DataAnnotations;
using Adapters.Gateways.Base;

namespace Adapters.Gateways.StudentAssistanceScholarship
{
    public class CreateStudentAssistanceScholarshipRequest : Request
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}