using Application.Ports.User;

namespace Application.Interfaces.UseCases.User
{
    public interface IGetUserById
    {
        Task<UserReadOutput> ExecuteAsync(Guid? id);
    }
}