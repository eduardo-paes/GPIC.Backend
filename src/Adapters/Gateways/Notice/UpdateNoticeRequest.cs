using Adapters.Gateways.Base;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Notice
{
    public class UpdateNoticeRequest : Request
    {
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? FinalDate { get; set; }

        public Guid? Id { get; set; }
        public string? Description { get; set; }
        public string? DocUrl { get; set; }
        public IFormFile? File { get; set; }
    }
}