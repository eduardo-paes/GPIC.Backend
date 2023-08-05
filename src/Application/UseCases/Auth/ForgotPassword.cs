using Domain.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UseCases.Auth;
using Application.Validation;

namespace Application.UseCases.Auth
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
            var user = await _userRepository.GetUserByEmailAsync(email)
                ?? throw UseCaseException.NotFoundEntityByParams(nameof(Domain.Entities.User));

            // Gera token de recuperação de senha
            user.GenerateResetPasswordToken();

            // Salva alterações
            await _userRepository.UpdateAsync(user);

            // Envia email de recuperação de senha
            await _emailService.SendResetPasswordEmailAsync(user.Email, user.Name, user.ResetPasswordToken);

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