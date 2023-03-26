using Domain.Contracts.User;

namespace Domain.Interfaces.User
{
    public interface IGetInactiveUsers
    {
        Task<IQueryable<UserReadOutput>> Execute(int skip, int take);
    }
}