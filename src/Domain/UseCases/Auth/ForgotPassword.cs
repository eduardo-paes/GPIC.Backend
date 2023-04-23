using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases.Auth;

namespace Domain.UseCases.Auth
{
    public class ForgotPassword : IForgotPassword
    {
        #region Global Scope
        private readonly IUserRepository _userRepository;
        public ForgotPassword(IUserRepository userRepository) => _userRepository = userRepository;
        #endregion

        public async Task<string> Execute(string? email)
        {
            // Verifica se o email é nulo
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email), "Email não informado.");

            // Busca usuário pelo email
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new Exception("Nenhum usuário encontrado para o email informado.");

            // Gera token de recuperação de senha
            user.GenerateResetPasswordToken();

            // Salva alterações
            await _userRepository.Update(user);

            // Retorna token
            return user.ResetPasswordToken ?? throw new Exception("Token não gerado.");
        }
    }
}