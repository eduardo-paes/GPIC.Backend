using Domain.UseCases.Ports.User;

namespace Domain.UseCases.Interfaces.User
{
    public interface IGetUserById
    {
        Task<UserReadOutput> ExecuteAsync(Guid? id);
    }
}