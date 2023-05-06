using Domain.Contracts.User;

namespace Domain.Interfaces.UseCases
{
    public interface IGetActiveUsers
    {
        Task<IEnumerable<UserReadOutput>> Execute(int skip, int take);
    }
}