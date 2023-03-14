using Application.DTOs.User;

namespace Application.Proxies.User
{
    public interface IGetActiveUsers
    {
        Task<IQueryable<UserReadDTO>> Execute(int skip, int take);
    }
}