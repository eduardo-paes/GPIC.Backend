using Application.DTOs.SubArea;

namespace Application.Proxies.SubArea
{
    public interface IGetSubAreasByArea
    {
        Task<IQueryable<ResumedReadSubAreaDTO>> Execute(Guid? areaId, int skip, int take);
    }
}