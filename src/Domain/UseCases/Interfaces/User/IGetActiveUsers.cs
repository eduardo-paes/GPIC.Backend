using Domain.UseCases.Ports.User;

namespace Domain.UseCases.Interfaces.User
{
    public interface IGetActiveUsers
    {
        Task<IEnumerable<UserReadOutput>> Execute(int skip, int take);
    }
}