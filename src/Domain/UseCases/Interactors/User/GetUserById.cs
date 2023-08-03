using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.User;
using Domain.UseCases.Ports.User;
using Domain.Validation;

namespace Domain.UseCases.Interactors.User
{
    public class GetUserById : IGetUserById
    {
        #region Global Scope
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public GetUserById(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<UserReadOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se o id informado é nulo
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Busca usuário pelo id informado
            Entities.User entity = await _repository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.User));

            // Retorna usuário encontrado
            return _mapper.Map<UserReadOutput>(entity);
        }
    }
}