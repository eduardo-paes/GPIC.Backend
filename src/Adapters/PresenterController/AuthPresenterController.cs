using Adapters.Gateways.Auth;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.Auth;
using Domain.Interfaces.UseCases;

namespace Adapters.PresenterController
{
    public class AuthPresenterController : IAuthPresenterController
    {
        #region Global Scope
        private readonly IMapper _mapper;
        private readonly IConfirmEmail _confirmUserEmail;
        private readonly IForgotPassword _forgotPassword;
        private readonly ILogin _login;
        private readonly IResetPassword _resetPassword;

        public AuthPresenterController(IMapper mapper, IConfirmEmail confirmUserEmail, IForgotPassword forgotPassword, ILogin login, IResetPassword resetPassword)
        {
            _mapper = mapper;
            _confirmUserEmail = confirmUserEmail;
            _forgotPassword = forgotPassword;
            _login = login;
            _resetPassword = resetPassword;
        }
        #endregion

        public async Task<string> ConfirmEmail(string? email, string? token) => await _confirmUserEmail.Execute(email, token);

        public async Task<string> ForgotPassword(string? email) => await _forgotPassword.Execute(email);

        public async Task<UserLoginResponse> Login(UserLoginRequest request) => _mapper
            .Map<UserLoginResponse>(await _login
                .Execute(_mapper
                    .Map<UserLoginInput>(request)));

        public async Task<string> ResetPassword(UserResetPasswordRequest request) => await _resetPassword.Execute(_mapper.Map<UserResetPasswordInput>(request));
    }
}