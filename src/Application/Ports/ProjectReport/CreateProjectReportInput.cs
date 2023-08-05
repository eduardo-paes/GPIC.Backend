using System.ComponentModel.DataAnnotations;
using Application.Ports.ProjectReport;
using Microsoft.AspNetCore.Http;

namespace Application.Ports.ProjectReport
{
    public class CreateProjectReportInput : BaseProjectReportContract
    {
        [Required]
        public IFormFile? ReportFile { get; set; }
    }
}