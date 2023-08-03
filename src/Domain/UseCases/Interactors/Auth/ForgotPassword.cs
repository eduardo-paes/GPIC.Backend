using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.UseCases.Interfaces.Auth;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Auth
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
        #endregion Global Scope

        public async Task<string> ExecuteAsync(string? email)
        {
            // Verifica se o email é nulo
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(email), nameof(email));

            // Busca usuário pelo email
            var user = await _userRepository.GetUserByEmail(email)
                ?? throw UseCaseException.NotFoundEntityByParams(nameof(Entities.User));

            // Gera token de recuperação de senha
            user.GenerateResetPasswordToken();

            // Salva alterações
            await _userRepository.Update(user);

            // Envia email de recuperação de senha
            await _emailService.SendResetPasswordEmail(user.Email, user.Name, user.ResetPasswordToken);

            // Verifica se o token foi gerado
            if (string.IsNullOrEmpty(user.ResetPasswordToken))
            {
                throw UseCaseException.BusinessRuleViolation("Token não gerado.");
            }

            // Retorna token
            return "Token de recuperação gerado e enviado por e-mail com sucesso.";
        }
    }
}