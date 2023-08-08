using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Interfaces.UseCases.User;
using Application.Ports.User;
using Application.Validation;

namespace Application.UseCases.User
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
        #endregion Global Scope

        public async Task<UserReadOutput> ExecuteAsync(UserUpdateInput input)
        {
            // Busca as claims do usuário autenticado
            var userClaims = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Verifica se o id informado é nulo
            UseCaseException.NotInformedParam(userClaims.Id is null, nameof(userClaims.Id));

            // Busca usuário pelo id informado
            Domain.Entities.User user = await _repository.GetByIdAsync(userClaims.Id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.User));

            // Atualiza atributos permitidos
            user.Name = input.Name;
            user.CPF = input.CPF;

            // Salva usuário atualizado no banco
            Domain.Entities.User entity = await _repository.UpdateAsync(user);
            return _mapper.Map<UserReadOutput>(entity);
        }
    }
}