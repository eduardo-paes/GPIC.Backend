using Adapters.Gateways.Auth;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.Auth;
using Domain.UseCases.Ports.Auth;

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
        #endregion Global Scope

        public async Task<string> ConfirmEmail(string? email, string? token)
        {
            return await _confirmUserEmail.Execute(email, token);
        }

        public async Task<string> ForgotPassword(string? email)
        {
            return await _forgotPassword.Execute(email);
        }

        public async Task<UserLoginResponse> Login(UserLoginRequest request)
        {
            return _mapper
            .Map<UserLoginResponse>(await _login
                .Execute(_mapper
                    .Map<UserLoginInput>(request)));
        }

        public async Task<string> ResetPassword(UserResetPasswordRequest request)
        {
            return await _resetPassword.Execute(_mapper.Map<UserResetPasswordInput>(request));
        }
    }
}