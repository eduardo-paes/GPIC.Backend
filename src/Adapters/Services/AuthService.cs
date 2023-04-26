using Adapters.DTOs.Auth;
using Adapters.Proxies.Auth;
using AutoMapper;
using Domain.Contracts.Auth;
using Domain.Interfaces.UseCases.Auth;

namespace Adapters.Services
{
    public class AuthService : IAuthService
    {
        #region Global Scope
        private readonly IMapper _mapper;
        private readonly IConfirmEmail _confirmUserEmail;
        private readonly IForgotPassword _forgotPassword;
        private readonly ILogin _login;
        private readonly IResetPassword _resetPassword;

        public AuthService(IMapper mapper, IConfirmEmail confirmUserEmail, IForgotPassword forgotPassword, ILogin login, IResetPassword resetPassword)
        {
            _mapper = mapper;
            _confirmUserEmail = confirmUserEmail;
            _forgotPassword = forgotPassword;
            _login = login;
            _resetPassword = resetPassword;
        }
        #endregion

        public async Task<string> ConfirmEmail(Guid? userId, string? token) => await _confirmUserEmail.Execute(userId, token);

        public async Task<string> ForgotPassword(string? email) => await _forgotPassword.Execute(email);

        public async Task<UserLoginResponseDTO> Login(UserLoginRequestDTO dto) => _mapper
            .Map<UserLoginResponseDTO>(await _login
                .Execute(_mapper
                    .Map<UserLoginInput>(dto)));

        public async Task<string> ResetPassword(UserResetPasswordDTO dto) => await _resetPassword.Execute(_mapper.Map<UserResetPasswordInput>(dto));
    }
}