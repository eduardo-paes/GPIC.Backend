using Adapters.DTOs.Base;
using Adapters.Proxies.Base;

namespace Adapters.Proxies.Area
{
    public interface IAreaService : IGenericCRUDService
    {
        Task<IQueryable<ResponseDTO>> GetAreasByMainArea(Guid? mainAreaId, int skip, int take);
    }
}