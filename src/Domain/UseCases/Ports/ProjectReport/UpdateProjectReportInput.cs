using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.UseCases.Ports.ProjectReport
{
    public class UpdateProjectReportInput
    {
        [Required]
        public IFormFile? ReportFile { get; set; }
    }
}