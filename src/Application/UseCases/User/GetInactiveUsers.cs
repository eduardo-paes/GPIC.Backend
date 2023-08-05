using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.User;
using Application.Ports.User;

namespace Application.UseCases.User
{
    public class GetInactiveUsers : IGetInactiveUsers
    {
        #region Global Scope
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public GetInactiveUsers(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IEnumerable<UserReadOutput>> ExecuteAsync(int skip, int take)
        {
            IEnumerable<Domain.Entities.User> entities = await _repository.GetInactiveUsersAsync(skip, take);
            return _mapper.Map<IEnumerable<UserReadOutput>>(entities).AsQueryable();
        }
    }
}