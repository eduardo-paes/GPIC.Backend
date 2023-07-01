using Domain.Contracts.Auth;
using Domain.Interfaces.UseCases;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Validation;

namespace Domain.UseCases
{
    public class Login : ILogin
    {
        #region Global Scope
        private readonly ITokenAuthenticationService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        public Login(ITokenAuthenticationService tokenService, IUserRepository userRepository, IHashService hashService)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _hashService = hashService;
        }
        #endregion

        public async Task<UserLoginOutput> Execute(UserLoginInput input)
        {
            // Verifica se o email é nulo
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Email), nameof(input.Email));

            // Verifica se a senha é nula
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Password), nameof(input.Password));

            // Busca usuário pelo email
            var entity = await _userRepository.GetUserByEmail(input.Email)
                ?? throw UseCaseException.NotFoundEntityByParams(nameof(Entities.User));

            // Verifica se o usuário está confirmado
            UseCaseException.BusinessRuleViolation(!entity.IsConfirmed, "User's email has not yet been confirmed.");

            // Verifica se a senha é válida
            UseCaseException.BusinessRuleViolation(!_hashService.VerifyPassword(input.Password!, entity.Password), "Invalid credentials.");

            // Gera o token de autenticação e retorna o resultado
            return _tokenService.GenerateToken(entity.Id, entity.Name, entity.Role.ToString());
        }
    }
}