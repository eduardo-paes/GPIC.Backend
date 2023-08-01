using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.User;
using Domain.UseCases.Ports.User;

namespace Domain.UseCases.Interactors.User
{
    public class GetActiveUsers : IGetActiveUsers
    {
        #region Global Scope
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public GetActiveUsers(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IEnumerable<UserReadOutput>> Execute(int skip, int take)
        {
            IEnumerable<Entities.User> entities = (IEnumerable<Entities.User>)await _repository.GetActiveUsers(skip, take);
            return _mapper.Map<IEnumerable<UserReadOutput>>(entities).AsQueryable();
        }
    }
}