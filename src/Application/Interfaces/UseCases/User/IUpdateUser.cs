using Application.Ports.User;

namespace Application.Interfaces.UseCases.User
{
    public interface IUpdateUser
    {
        Task<UserReadOutput> ExecuteAsync(UserUpdateInput input);
    }
}