using Application.DTOs.User;

namespace Application.Proxies.User
{
    public interface IGetInactiveUsers
    {
        Task<IQueryable<UserReadDTO>> Execute(int skip, int take);
    }
}