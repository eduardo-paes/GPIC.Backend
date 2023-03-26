using Domain.Contracts.User;

namespace Domain.Interfaces.User
{
    public interface IUpdateUser
    {
        Task<UserReadOutput> Execute(Guid? id, UserUpdateInput user);
    }
}