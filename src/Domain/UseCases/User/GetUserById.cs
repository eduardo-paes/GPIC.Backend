using AutoMapper;
using Domain.Contracts.User;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;
using Domain.Validation;

namespace Domain.UseCases
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
        #endregion

        public async Task<UserReadOutput> Execute(Guid? id)
        {
            // Verifica se o id informado é nulo
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Busca usuário pelo id informado
            var entity = await _repository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.User));

            // Retorna usuário encontrado
            return _mapper.Map<UserReadOutput>(entity);
        }
    }
}