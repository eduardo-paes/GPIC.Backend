using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.Ports.ProjectFinalReport
{
    public class UpdateProjectFinalReportInput
    {
        [Required]
        public IFormFile? ReportFile { get; set; }
    }
}