using Domain.Contracts.User;

namespace Domain.Interfaces.UseCases
{
    public interface IDeactivateUser
    {
        Task<UserReadOutput> Execute(Guid? id);
    }
}