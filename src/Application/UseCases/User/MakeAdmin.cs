using Application.Interfaces.UseCases.User;
using Application.Validation;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases.User
{
    public class MakeAdmin : IMakeAdmin
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        public MakeAdmin(
            IUserRepository userRepository,
            IProfessorRepository professorRepository,
            ITokenAuthenticationService tokenAuthenticationService)
        {
            _userRepository = userRepository;
            _professorRepository = professorRepository;
            _tokenAuthenticationService = tokenAuthenticationService;
        }

        public async Task<string> ExecuteAsync(Guid? userId)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(userId is null,
                nameof(userId));

            // Obtém usuário logado
            var userClaims = _tokenAuthenticationService.GetUserAuthenticatedClaims();
            UseCaseException.BusinessRuleViolation(userClaims is null,
                "Usuário não autenticado");

            // Verifica se usuário logado é administrador
            UseCaseException.BusinessRuleViolation(userClaims!.Role != Domain.Entities.Enums.ERole.ADMIN,
                "Usuário logado não é administrador");

            // Verifica se usuário logado realmente existe
            var user = await _userRepository.GetByIdAsync(userClaims.Id);
            UseCaseException.NotFoundEntityById(user is null, nameof(user));

            // Obtém usuário que será tornado administrador
            var userToMakeAdmin = await _userRepository.GetByIdAsync(userId);

            // Verifica se usuário que será tornado administrador existe
            UseCaseException.NotFoundEntityById(userToMakeAdmin is null,
                nameof(userToMakeAdmin));

            /// Verifica se usuário que será tornado administrador é professor,
            /// pois apenas um professor pode se tornar administrador
            _ = await _professorRepository.GetByUserIdAsync(userId)
                ?? throw new UseCaseException("Apenas professores podem se tornar administradores");

            // Atualiza usuário
            userToMakeAdmin!.Role = Domain.Entities.Enums.ERole.ADMIN;

            // Salva alterações
            await _userRepository.UpdateAsync(userToMakeAdmin);

            // Retorna mensagem de sucesso
            return $"Usuário {user!.Id} tornou administrador o usuário {userToMakeAdmin.Id}";
        }
    }
}