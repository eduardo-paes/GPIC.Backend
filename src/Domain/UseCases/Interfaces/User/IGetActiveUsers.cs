using Domain.UseCases.Ports.User;

namespace Domain.UseCases.Interfaces.User
{
    public interface IGetActiveUsers
    {
        Task<IEnumerable<UserReadOutput>> ExecuteAsync(int skip, int take);
    }
}