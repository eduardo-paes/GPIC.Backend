using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;
using Domain.Validation;

namespace Domain.UseCases
{
    public class ConfirmEmail : IConfirmEmail
    {
        #region Global Scope
        private readonly IUserRepository _userRepository;
        public ConfirmEmail(IUserRepository userRepository) => _userRepository = userRepository;
        #endregion

        public async Task<string> Execute(string? email, string? token)
        {
            // Verifica se o email é nulo
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(email), nameof(email));

            // Verifica se o token é nulo
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(token), nameof(token));

            // Busca usuário pelo email informado
            var user = await _userRepository.GetUserByEmail(email)
                ?? throw UseCaseException.NotFoundEntityByParams(nameof(Entities.User));

            // Confirma usuário
            user.ConfirmUserEmail(token!);

            // Atualiza usuário
            await _userRepository.Update(user);

            // Retorna mensagem de sucesso
            return "Usuário confirmado com sucesso.";
        }
    }
}