using Adapters.DTOs.Base;
using Adapters.Proxies.Base;

namespace Adapters.Proxies.SubArea
{
    public interface ISubAreaService : IGenericCRUDService
    {
        Task<IQueryable<ResponseDTO>> GetSubAreasByArea(Guid? areaId, int skip, int take);
    }
}