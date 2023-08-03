using Domain.UseCases.Ports.User;

namespace Domain.UseCases.Interfaces.User
{
    public interface IUpdateUser
    {
        Task<UserReadOutput> ExecuteAsync(UserUpdateInput input);
    }
}