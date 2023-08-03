using Domain.UseCases.Ports.User;

namespace Domain.UseCases.Interfaces.User
{
    public interface IGetInactiveUsers
    {
        Task<IEnumerable<UserReadOutput>> ExecuteAsync(int skip, int take);
    }
}