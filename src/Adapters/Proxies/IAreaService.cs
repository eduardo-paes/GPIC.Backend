using Adapters.DTOs.Base;
using Adapters.Proxies.Base;

namespace Adapters.Proxies;
public interface IAreaService : IGenericCRUDService
{
    Task<IEnumerable<ResponseDTO>> GetAreasByMainArea(Guid? mainAreaId, int skip, int take);
}