using System.ComponentModel.DataAnnotations;
using Domain.UseCases.Ports.ProjectReport;
using Microsoft.AspNetCore.Http;

namespace Domain.Ports.ProjectReport
{
    public class CreateProjectReportInput : BaseProjectReportContract
    {
        [Required]
        public IFormFile? ReportFile { get; set; }
    }
}