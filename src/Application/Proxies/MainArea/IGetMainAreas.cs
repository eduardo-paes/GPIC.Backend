using Application.DTOs.MainArea;

namespace Application.Proxies.MainArea
{
    public interface IGetMainAreas
    {
        Task<IQueryable<ResumedReadMainAreaDTO>> Execute(int skip, int take);
    }
}