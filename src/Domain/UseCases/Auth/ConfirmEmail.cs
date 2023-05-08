using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;

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
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email), "Email não informado.");

            // Verifica se o token é nulo
            else if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token), "Token não informado.");

            // Busca usuário pelo email informado
            var user = await _userRepository.GetUserByEmail(email)
                ?? throw new Exception("Usuário não encontrado.");

            // Confirma usuário
            user.ConfirmUserEmail(token);

            // Atualiza usuário
            await _userRepository.Update(user);

            // Retorna mensagem de sucesso
            return "Usuário confirmado com sucesso.";
        }
    }
}