using Domain.Contracts.User;

namespace Domain.Interfaces.User
{
    public interface IGetUserById
    {
        Task<UserReadOutput> Execute(Guid? id);
    }
}