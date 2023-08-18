using System.ComponentModel.DataAnnotations;
using Application.Ports.ProjectFinalReport;
using Microsoft.AspNetCore.Http;

namespace Application.Ports.ProjectFinalReport
{
    public class CreateProjectFinalReportInput : BaseProjectFinalReportContract
    {
        [Required]
        public IFormFile? ReportFile { get; set; }
    }
}