using Microsoft.AspNetCore.Http;

namespace Domain.Contracts.Notice
{
    public class CreateNoticeInput : BaseNoticeContract
    {
        public IFormFile? File { get; set; }
    }
}