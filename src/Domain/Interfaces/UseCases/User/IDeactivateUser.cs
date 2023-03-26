using Domain.Contracts.User;

namespace Domain.Interfaces.User
{
    public interface IDeactivateUser
    {
        Task<UserReadOutput> Execute(Guid id);
    }
}