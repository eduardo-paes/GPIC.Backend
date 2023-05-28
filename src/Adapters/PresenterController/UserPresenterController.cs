using Adapters.Gateways.User;
using Adapters.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserPresenterController(IActivateUser activateUser,
            IDeactivateUser deactivateUser,
            IGetActiveUsers getActiveUsers,
            IGetInactiveUsers getInactiveUsers,
            IGetUserById getUserById,
            IUpdateUser updateUser,
            IMapper mapper)
        {
            _activateUser = activateUser;
            _deactivateUser = deactivateUser;
            _getActiveUsers = getActiveUsers;
            _getInactiveUsers = getInactiveUsers;
            _getUserById = getUserById;
            _updateUser = updateUser;
            _mapper = mapper;
        }
        #endregion

        public async Task<UserReadResponse> ActivateUser(Guid? id)
        {
            var result = await _activateUser.Execute(id);
            return _mapper.Map<UserReadResponse>(result);
        }

        public async Task<UserReadResponse> DeactivateUser(Guid? id)
        {
            var result = await _deactivateUser.Execute(id);
            return _mapper.Map<UserReadResponse>(result);
        }

        public async Task<IEnumerable<UserReadResponse>> GetActiveUsers(int skip, int take)
        {
            var result = await _getActiveUsers.Execute(skip, take);
            return _mapper.Map<IEnumerable<UserReadResponse>>(result);
        }

        public async Task<IEnumerable<UserReadResponse>> GetInactiveUsers(int skip, int take)
        {
            var result = await _getInactiveUsers.Execute(skip, take);
            return _mapper.Map<IEnumerable<UserReadResponse>>(result);
        }

        public async Task<UserReadResponse> GetUserById(Guid? id)
        {
            var result = await _getUserById.Execute(id);
            return _mapper.Map<UserReadResponse>(result);
        }

        public async Task<UserReadResponse> UpdateUser(UserUpdateRequest request)
        {
            var input = _mapper.Map<UserUpdateInput>(request);
            var result = await _updateUser.Execute(input);
            return _mapper.Map<UserReadResponse>(result);
        }
    }
}