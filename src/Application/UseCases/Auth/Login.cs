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
        private readonly IProfessorRepository _professorRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IHashService _hashService;
        public Login(
            ITokenAuthenticationService tokenService,
            IUserRepository userRepository,
            IProfessorRepository professorRepository,
            IStudentRepository studentRepository,
            IHashService hashService)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _professorRepository = professorRepository;
            _studentRepository = studentRepository;
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
            var user = await _userRepository.GetUserByEmailAsync(input.Email)
                ?? throw UseCaseException.NotFoundEntityByParams(nameof(Domain.Entities.User));

            // Verifica se o usuário está confirmado
            UseCaseException.BusinessRuleViolation(!user.IsConfirmed, "O e-mail do usuário ainda não foi confirmado.");

            // Verifica se a senha é válida
            UseCaseException.BusinessRuleViolation(!_hashService.VerifyPassword(input.Password!, user.Password), "Credenciais inválidas.");

            // Obtém id do professor ou do aluno
            Guid? actorId;
            if (user.Role == Domain.Entities.Enums.ERole.PROFESSOR)
            {
                var professor = await _professorRepository.GetByUserIdAsync(user.Id)
                    ?? throw UseCaseException.NotFoundEntityByParams(nameof(Domain.Entities.Professor));
                actorId = professor.Id;
            }
            else if (user.Role == Domain.Entities.Enums.ERole.STUDENT)
            {
                var student = await _studentRepository.GetByUserIdAsync(user.Id)
                    ?? throw UseCaseException.NotFoundEntityByParams(nameof(Domain.Entities.Student));
                actorId = student.Id;
            }
            else actorId = null;

            // Gera o token de autenticação e retorna o resultado
            return new UserLoginOutput
            {
                Token = _tokenService.GenerateToken(user.Id, actorId, user.Name, user.Role.ToString())
            };
        }
    }
}