using AutoMapper;
using Domain.Contracts.User;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;

namespace Domain.UseCases
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
        #endregion

        public async Task<IEnumerable<UserReadOutput>> Execute(int skip, int take)
        {
            var entities = await _repository.GetActiveUsers(skip, take);
            return _mapper.Map<IEnumerable<UserReadOutput>>(entities).AsQueryable();
        }
    }
}