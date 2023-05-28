using Domain.Contracts.User;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateUser
    {
        Task<UserReadOutput> Execute(UserUpdateInput input);
    }
}