using Application.Ports.User;

namespace Application.Interfaces.UseCases.User
{
    public interface IActivateUser
    {
        Task<UserReadOutput> ExecuteAsync(Guid? id);
    }
}