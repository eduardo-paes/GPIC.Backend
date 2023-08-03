using Domain.UseCases.Ports.User;

namespace Domain.UseCases.Interfaces.User
{
    public interface IActivateUser
    {
        Task<UserReadOutput> ExecuteAsync(Guid? id);
    }
}