using Adapters.Gateways.User;
using Adapters.Interfaces;
using Domain.Contracts.User;
using Domain.Interfaces.UseCases;

namespace Adapters.PresenterController
{
    public class UserPresenterController : IUserPresenterController
    {
        #region Global Scope
        private readonly IActivateUser _activateUser;
        private readonly IDeactivateUser _deactivateUser;
        private readonly IGetActiveUsers _getActiveUsers;
        private readonly IGetInactiveUsers _getInactiveUsers;
        private readonly IGetUserById _getUserById;
        private readonly IUpdateUser _updateUser;
        public UserPresenterController(IActivateUser activateUser,
            IDeactivateUser deactivateUser,
            IGetActiveUsers getActiveUsers,
            IGetInactiveUsers getInactiveUsers,
            IGetUserById getUserById,
            IUpdateUser updateUser)
        {
            _activateUser = activateUser;
            _deactivateUser = deactivateUser;
            _getActiveUsers = getActiveUsers;
            _getInactiveUsers = getInactiveUsers;
            _getUserById = getUserById;
            _updateUser = updateUser;
        }
        #endregion

        public async Task<UserReadResponse?> ActivateUser(Guid? id) => await _activateUser.Execute(id) as UserReadResponse;
        public async Task<UserReadResponse?> DeactivateUser(Guid? id) => await _deactivateUser.Execute(id) as UserReadResponse;
        public async Task<IEnumerable<UserReadResponse>?> GetActiveUsers(int skip, int take) => await _getActiveUsers.Execute(skip, take) as IEnumerable<UserReadResponse>;
        public async Task<IEnumerable<UserReadResponse>?> GetInactiveUsers(int skip, int take) => await _getInactiveUsers.Execute(skip, take) as IEnumerable<UserReadResponse>;
        public async Task<UserReadResponse?> GetUserById(Guid? id) => await _getUserById.Execute(id) as UserReadResponse;
        public async Task<UserReadResponse?> UpdateUser(UserUpdateRequest request) => await _updateUser.Execute((request as UserUpdateInput)!) as UserReadResponse;
    }
}