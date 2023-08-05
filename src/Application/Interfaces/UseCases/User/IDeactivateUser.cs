using Application.Ports.User;

namespace Application.Interfaces.UseCases.User
{
    public interface IDeactivateUser
    {
        Task<UserReadOutput> ExecuteAsync(Guid? id);
    }
}