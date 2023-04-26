using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases.Auth;

namespace Domain.UseCases.Auth
{
    public class ConfirmEmail : IConfirmEmail
    {
        #region Global Scope
        private readonly IUserRepository _userRepository;
        public ConfirmEmail(IUserRepository userRepository) => _userRepository = userRepository;
        #endregion

        public async Task<string> Execute(Guid? userId, string? token)
        {
            // Verifica se userId é nulo
            if (userId == null)
                throw new ArgumentNullException(nameof(userId), "Id do usuário não informado.");

            // Verifica se token é nulo
            else if (token == null)
                throw new ArgumentNullException(nameof(token), "Token não informado.");

            // Busca usuário pelo id
            var user = await _userRepository.GetById(userId.Value);
            if (user == null)
                throw new Exception("Usuário não encontrado.");

            // Confirma usuário
            user.ConfirmUserEmail(token);

            // Atualiza usuário
            await _userRepository.Update(user);

            // Retorna mensagem de sucesso
            return "Usuário confirmado com sucesso.";
        }
    }
}