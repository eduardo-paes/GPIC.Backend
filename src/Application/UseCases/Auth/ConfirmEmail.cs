using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Auth;
using Application.Validation;

namespace Application.UseCases.Auth
{
    public class ConfirmEmail : IConfirmEmail
    {
        #region Global Scope
        private readonly IUserRepository _userRepository;
        public ConfirmEmail(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion Global Scope

        public async Task<string> ExecuteAsync(string? email, string? token)
        {
            // Verifica se o email é nulo
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(email), nameof(email));

            // Verifica se o token é nulo
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(token), nameof(token));

            // Busca usuário pelo email informado
            var user = await _userRepository.GetUserByEmailAsync(email)
                ?? throw UseCaseException.NotFoundEntityByParams(nameof(Domain.Entities.User));

            // Confirma usuário
            user.ConfirmUserEmail(token!);

            // Atualiza usuário
            await _userRepository.UpdateAsync(user);

            // Retorna mensagem de sucesso
            return "Usuário confirmado com sucesso.";
        }
    }
}