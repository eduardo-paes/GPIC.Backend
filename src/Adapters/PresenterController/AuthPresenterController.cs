using Adapters.Gateways.Auth;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Interfaces.UseCases;

namespace Adapters.PresenterController
{
    public class AuthPresenterController : IAuthPresenterController
    {
        #region Global Scope
        private readonly IConfirmEmail _confirmUserEmail;
        private readonly IForgotPassword _forgotPassword;
        private readonly ILogin _login;
        private readonly IResetPassword _resetPassword;

        public AuthPresenterController(IConfirmEmail confirmUserEmail,
            IForgotPassword forgotPassword,
            ILogin login,
            IResetPassword resetPassword)
        {
            _confirmUserEmail = confirmUserEmail;
            _forgotPassword = forgotPassword;
            _login = login;
            _resetPassword = resetPassword;
        }
        #endregion

        public async Task<string> ForgotPassword(string? email) => await _forgotPassword.Execute(email);
        public async Task<string> ResetPassword(UserResetPasswordRequest request) => await _resetPassword.Execute(request);
        public async Task<string> ConfirmEmail(string? email, string? token) => await _confirmUserEmail.Execute(email, token);
        public async Task<UserLoginResponse> Login(UserLoginRequest request) => (await _login.Execute(request) as UserLoginResponse)!;
    }
}