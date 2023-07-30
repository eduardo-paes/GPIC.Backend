using Domain.Contracts.Activity;
using Microsoft.AspNetCore.Http;

namespace Domain.Contracts.Notice
{
    public class UpdateNoticeInput : BaseNoticeContract
    {
        public Guid? Id { get; set; }
        public IFormFile? File { get; set; }
        public IList<UpdateActivityTypeInput>? Activities { get; set; }
    }
}