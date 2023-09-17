using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Interfaces.UseCases.Auth;
using Application.Ports.Auth;
using Application.Validation;

namespace Application.UseCases.Auth
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
        #endregion Global Scope

        public async Task<UserLoginOutput> ExecuteAsync(UserLoginInput input)
        {
            // Verifica se o email é nulo
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Email), nameof(input.Email));

            // Verifica se a senha é nula
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Password), nameof(input.Password));

            // Busca usuário pelo email
            var entity = await _userRepository.GetUserByEmailAsync(input.Email)
                ?? throw UseCaseException.NotFoundEntityByParams(nameof(Domain.Entities.User));

            // Verifica se o usuário está confirmado
            UseCaseException.BusinessRuleViolation(!entity.IsConfirmed, "O e-mail do usuário ainda não foi confirmado.");

            // Verifica se a senha é válida
            UseCaseException.BusinessRuleViolation(!_hashService.VerifyPassword(input.Password!, entity.Password), "Credenciais inválidas.");

            // Gera o token de autenticação e retorna o resultado
            return new UserLoginOutput
            {
                Token = _tokenService.GenerateToken(entity.Id, entity.Name, entity.Role.ToString())
            };
        }
    }
}