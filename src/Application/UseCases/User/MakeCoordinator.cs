using Application.Interfaces.UseCases.User;
using Application.Validation;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases.User
{
    public class MakeCoordinator : IMakeCoordinator
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        public MakeCoordinator(
            IUserRepository userRepository,
            ITokenAuthenticationService tokenAuthenticationService)
        {
            _userRepository = userRepository;
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

            // Obtém id do usuário e id de acordo com perfil logado
            var userClaim = userClaims!.Values.FirstOrDefault();

            // Verifica se usuário logado é administrador
            UseCaseException.BusinessRuleViolation(userClaim!.Role != Domain.Entities.Enums.ERole.ADMIN,
                "Usuário não é administrador");

            // Verifica se usuário logado realmente existe
            var user = await _userRepository.GetByIdAsync(userClaim.Id);
            UseCaseException.NotFoundEntityById(user is null, nameof(user));

            // Verifica se usuário logado é coordenador
            UseCaseException.BusinessRuleViolation(user!.IsCoordinator,
                "Usuário não é coordenador");

            // Obtém usuário que será tornado coordenador
            var userToMakeCoordinator = await _userRepository.GetByIdAsync(userId);

            // Verifica se usuário que será tornado coordenador existe
            UseCaseException.NotFoundEntityById(userToMakeCoordinator is null,
                nameof(userToMakeCoordinator));

            // Remove coordenação do usuário logado
            user.IsCoordinator = false;

            // Salva alterações
            await _userRepository.UpdateAsync(user);

            // Atualiza usuário
            userToMakeCoordinator!.IsCoordinator = true;

            // Salva alterações
            await _userRepository.UpdateAsync(userToMakeCoordinator);

            // Retorna mensagem de sucesso
            return $"Usuário {user.Id} tornou coordenador o usuário {userToMakeCoordinator.Id}";
        }
    }
}