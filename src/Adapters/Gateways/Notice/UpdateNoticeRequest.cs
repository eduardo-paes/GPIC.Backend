using Adapters.Gateways.Activity;
using Adapters.Gateways.Base;
using Domain.UseCases.Ports.Notice;
using Microsoft.AspNetCore.Http;

namespace Adapters.Gateways.Notice
{
    public class UpdateNoticeRequest : BaseNoticeContract, IRequest
    {
        public Guid? Id { get; set; }
        public IFormFile? File { get; set; }
        public IList<UpdateActivityTypeRequest>? Activities { get; set; }
    }
}