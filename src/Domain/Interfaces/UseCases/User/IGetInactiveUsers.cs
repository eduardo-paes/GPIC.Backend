using Domain.Contracts.User;

namespace Domain.Interfaces.UseCases
{
    public interface IGetInactiveUsers
    {
        Task<IEnumerable<UserReadOutput>> Execute(int skip, int take);
    }
}