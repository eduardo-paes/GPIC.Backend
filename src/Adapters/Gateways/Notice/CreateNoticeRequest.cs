using Adapters.Gateways.Base;
using Domain.Contracts.Notice;
using Microsoft.AspNetCore.Http;

namespace Adapters.Gateways.Notice;
public class CreateNoticeRequest : BaseNoticeContract, IRequest
{
    public IFormFile? File { get; set; }
    public IList<CreateActivityTypeRequest>? Activities { get; set; }
}
