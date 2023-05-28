using AutoMapper;
using Domain.Contracts.User;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces.UseCases;

namespace Domain.UseCases
{
    public class UpdateUser : IUpdateUser
    {
        #region Global Scope
        private readonly IUserRepository _repository;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly IMapper _mapper;
        public UpdateUser(IUserRepository repository, ITokenAuthenticationService tokenAuthenticationService, IMapper mapper)
        {
            _repository = repository;
            _tokenAuthenticationService = tokenAuthenticationService;
            _mapper = mapper;
        }
        #endregion

        public async Task<UserReadOutput> Execute(UserUpdateInput input)
        {
            // Busca as claims do usuário autenticado
            var userClaims = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Verifica se o id informado é nulo
            if (userClaims.Id == null)
                throw new ArgumentNullException(nameof(userClaims.Id));

            // Busca usuário pelo id informado
            var user = await _repository.GetById(userClaims.Id)
                ?? throw new Exception("Nenhum usuário encontrato para o id informado.");

            // Atualiza atributos permitidos
            user.Name = input.Name;
            user.CPF = input.CPF;

            // Salva usuário atualizado no banco
            var entity = await _repository.Update(user);
            return _mapper.Map<UserReadOutput>(entity);
        }
    }
}