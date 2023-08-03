using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.User;
using Domain.UseCases.Ports.User;

namespace Domain.UseCases.Interactors.User
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
            IEnumerable<Entities.User> entities = (IEnumerable<Entities.User>)await _repository.GetInactiveUsers(skip, take);
            return _mapper.Map<IEnumerable<UserReadOutput>>(entities).AsQueryable();
        }
    }
}