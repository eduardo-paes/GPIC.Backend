using Application.Ports.Activity;
using Microsoft.AspNetCore.Http;

namespace Application.Ports.Notice
{
    public class CreateNoticeInput : BaseNoticeContract
    {
        public IFormFile? File { get; set; }
        public IList<CreateActivityTypeInput>? Activities { get; set; }
    }
}