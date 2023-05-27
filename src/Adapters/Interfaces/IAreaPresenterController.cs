using Adapters.Gateways.Base;
using Adapters.Interfaces.Base;

namespace Adapters.Interfaces;
public interface IAreaPresenterController : IGenericCRUDPresenterController
{
    Task<IEnumerable<Response>> GetAreasByMainArea(Guid? mainAreaId, int skip, int take);
}