using Application.DTOs.Area;

namespace Application.Proxies.Area
{
    public interface IGetAreasByMainArea
    {
        Task<IQueryable<ResumedReadAreaDTO>> Execute(Guid? mainAreaId, int skip, int take);
    }
}