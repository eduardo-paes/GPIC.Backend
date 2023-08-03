using Domain.UseCases.Ports.Activity;
using Microsoft.AspNetCore.Http;

namespace Domain.UseCases.Ports.Notice
{
    public class CreateNoticeInput : BaseNoticeContract
    {
        public IFormFile? File { get; set; }
        public IList<CreateActivityTypeInput>? Activities { get; set; }
    }
}