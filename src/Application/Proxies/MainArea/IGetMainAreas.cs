using Application.DTOs.MainArea;

namespace Application.Proxies.MainArea
{
    public interface IGetMainAreas
    {
        Task<IQueryable<ReadMainAreaDTO>> Execute(int skip, int take);
    }
}