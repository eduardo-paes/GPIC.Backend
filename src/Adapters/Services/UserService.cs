using Adapters.DTOs.User;
using Adapters.Proxies;
using AutoMapper;
using Domain.Contracts.User;
using Domain.Interfaces.UseCases;

namespace Adapters.Services
{
    public class UserService : IUserService
    {
        #region Global Scope
        private readonly IActivateUser _activateUser;
        private readonly IDeactivateUser _deactivateUser;
        private readonly IGetActiveUsers _getActiveUsers;
        private readonly IGetInactiveUsers _getInactiveUsers;
        private readonly IGetUserById _getUserById;
        private readonly IUpdateUser _updateUser;
        private readonly IMapper _mapper;
        public UserService(IActivateUser activateUser,
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

        public async Task<UserReadDTO> ActivateUser(Guid? id)
        {
            var result = await _activateUser.Execute(id);
            return _mapper.Map<UserReadDTO>(result);
        }

        public async Task<UserReadDTO> DeactivateUser(Guid? id)
        {
            var result = await _deactivateUser.Execute(id);
            return _mapper.Map<UserReadDTO>(result);
        }

        public async Task<IQueryable<UserReadDTO>> GetActiveUsers(int skip, int take)
        {
            var result = await _getActiveUsers.Execute(skip, take);
            return _mapper.Map<IQueryable<UserReadDTO>>(result);
        }

        public async Task<IQueryable<UserReadDTO>> GetInactiveUsers(int skip, int take)
        {
            var result = await _getInactiveUsers.Execute(skip, take);
            return _mapper.Map<IQueryable<UserReadDTO>>(result);
        }

        public async Task<UserReadDTO> GetUserById(Guid? id)
        {
            var result = await _getUserById.Execute(id);
            return _mapper.Map<UserReadDTO>(result);
        }

        public async Task<UserReadDTO> UpdateUser(Guid? id, UserUpdateDTO dto)
        {
            var input = _mapper.Map<UserUpdateInput>(dto);
            var result = await _updateUser.Execute(id, input);
            return _mapper.Map<UserReadDTO>(result);
        }
    }
}