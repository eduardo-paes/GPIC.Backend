using AutoMapper;
using Domain.Contracts.User;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;

namespace Domain.UseCases.User
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
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Busca usuário pelo id informado
            var entity = await _repository.GetById(id)
                ?? throw new Exception("Nenhum usuário encontrato para o id informado.");

            // Retorna usuário encontrado
            return _mapper.Map<UserReadOutput>(entity);
        }
    }
}