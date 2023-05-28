using Adapters.Gateways.Base;
using Adapters.Interfaces.Base;

namespace Adapters.Interfaces;
public interface ISubAreaPresenterController : IGenericCRUDPresenterController
{
    Task<IQueryable<Response>> GetSubAreasByArea(Guid? areaId, int skip, int take);
}