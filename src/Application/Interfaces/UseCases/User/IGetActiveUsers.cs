using Application.Ports.User;

namespace Application.Interfaces.UseCases.User
{
    public interface IGetActiveUsers
    {
        Task<IEnumerable<UserReadOutput>> ExecuteAsync(int skip, int take);
    }
}