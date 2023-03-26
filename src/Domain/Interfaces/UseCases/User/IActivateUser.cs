using Domain.Contracts.User;

namespace Domain.Interfaces.User
{
    public interface IActivateUser
    {
        Task<UserReadOutput> Execute(Guid id);
    }
}