using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.User;
using Application.Ports.User;

namespace Application.UseCases.User
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

        public async Task<IEnumerable<UserReadOutput>> ExecuteAsync(int skip, int take)
        {
            // Valida valores de skip e take
            if (skip < 0 || take < 1)
                throw new ArgumentException("Parâmetros inválidos.");

            IEnumerable<Domain.Entities.User> entities = await _repository.GetActiveUsersAsync(skip, take);
            return _mapper.Map<IEnumerable<UserReadOutput>>(entities).AsQueryable();
        }
    }
}