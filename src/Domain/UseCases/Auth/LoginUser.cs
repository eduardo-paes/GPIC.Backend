using Domain.Contracts.Auth;
using Domain.Interfaces.UseCases.Auth;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.UseCases.Auth
{
    public class LoginUser : ILoginUser
    {
        #region Global Scope
        private readonly ITokenAuthenticationService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        private readonly IMapper _mapper;
        public LoginUser(ITokenAuthenticationService tokenService, IUserRepository userRepository, IHashService hashService, IMapper mapper)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _hashService = hashService;
            _mapper = mapper;
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

            // Verifica se a senha é válida
            if (!_hashService.VerifyPassword(dto.Password, entity.Password))
                throw new Exception("Credenciais inválidas.");

            // Mapeia o resultado
            var model = _mapper.Map<UserLoginOutput>(entity);

            // Gera o token de autenticação
            model.Token = _tokenService.GenerateToken(model.Id, model.Role);

            // Retorna o resultado
            return model;
        }
    }
}