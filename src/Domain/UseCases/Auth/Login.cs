using Domain.Contracts.Auth;
using Domain.Interfaces.UseCases;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

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

        public async Task<UserLoginOutput> Execute(UserLoginInput dto)
        {
            // Verifica se o email é nulo
            if (string.IsNullOrEmpty(dto.Email))
                throw new Exception("Email não informado.");

            // Verifica se a senha é nula
            else if (string.IsNullOrEmpty(dto.Password))
                throw new Exception("Senha não informada.");

            // Busca usuário pelo email
            var entity = await _userRepository.GetUserByEmail(dto.Email);
            if (entity == null)
                throw new Exception("Nenhum usuário encontrado.");

            // Verifica se o usuário está confirmado
            if (!entity.IsConfirmed)
                throw new Exception("O e-mail do usuário ainda não foi confirmado.");

            // Verifica se a senha é válida
            if (!_hashService.VerifyPassword(dto.Password, entity.Password))
                throw new Exception("Credenciais inválidas.");

            // Gera o token de autenticação e retorna o resultado
            return _tokenService.GenerateToken(entity.Id, entity.Name, entity.Role.ToString());
        }
    }
}