using Adapters.Gateways.Base;
using Adapters.Interfaces.Base;

namespace Adapters.Interfaces;
public interface ISubAreaPresenterController : IGenericCRUDPresenterController
{
    Task<IEnumerable<IResponse>> GetSubAreasByArea(Guid? areaId, int skip, int take);
}
