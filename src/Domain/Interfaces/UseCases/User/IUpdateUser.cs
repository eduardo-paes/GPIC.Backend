using Domain.Contracts.User;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateUser
    {
        Task<UserReadOutput> Execute(Guid? id, UserUpdateInput input);
    }
}