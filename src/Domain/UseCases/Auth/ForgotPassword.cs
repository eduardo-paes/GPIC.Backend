using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces.UseCases;

namespace Domain.UseCases
{
    public class ForgotPassword : IForgotPassword
    {
        #region Global Scope
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public ForgotPassword(IUserRepository userRepository, IEmailService emailService)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }
        #endregion

        public async Task<string> Execute(string? email)
        {
            // Verifica se o email é nulo
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email), "Email não informado.");

            // Busca usuário pelo email
            var user = await _userRepository.GetUserByEmail(email)
                ?? throw new Exception("Nenhum usuário encontrado para o email informado.");

            // Gera token de recuperação de senha
            user.GenerateResetPasswordToken();

            // Salva alterações
            await _userRepository.Update(user);

            // Envia email de recuperação de senha
            await _emailService.SendResetPasswordEmail(user.Email, user.Name, user.ResetPasswordToken);

            // Verifica se o token foi gerado
            if (string.IsNullOrEmpty(user.ResetPasswordToken))
                throw new Exception("Token não gerado.");

            // Retorna token
            return "Token de recuperação gerado e enviado por e-mail com sucesso.";
        }
    }
}