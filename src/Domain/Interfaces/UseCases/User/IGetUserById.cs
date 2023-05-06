using Domain.Contracts.User;

namespace Domain.Interfaces.UseCases
{
    public interface IGetUserById
    {
        Task<UserReadOutput> Execute(Guid? id);
    }
}