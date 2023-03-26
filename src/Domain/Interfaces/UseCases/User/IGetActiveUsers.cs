using Domain.Contracts.User;

namespace Domain.Interfaces.User
{
    public interface IGetActiveUsers
    {
        Task<IQueryable<UserReadOutput>> Execute(int skip, int take);
    }
}