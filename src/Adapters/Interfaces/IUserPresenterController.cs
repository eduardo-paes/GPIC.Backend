using Adapters.Gateways.User;

namespace Adapters.Interfaces
{
    public interface IUserPresenterController
    {
        Task<UserReadResponse> ActivateUser(Guid? id);
        Task<UserReadResponse> DeactivateUser(Guid? id);
        Task<IEnumerable<UserReadResponse>> GetActiveUsers(int skip, int take);
        Task<IEnumerable<UserReadResponse>> GetInactiveUsers(int skip, int take);
        Task<UserReadResponse> GetUserById(Guid? id);
        Task<UserReadResponse> UpdateUser(UserUpdateRequest request);
    }
}