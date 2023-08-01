using Domain.UseCases.Ports.User;

namespace Domain.UseCases.Interfaces.User
{
    public interface IDeactivateUser
    {
        Task<UserReadOutput> Execute(Guid? id);
    }
}