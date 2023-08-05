using Application.Ports.Auth;
using Application.Interfaces.UseCases.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Autenticação.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Global Scope
        private readonly IConfirmEmail _confirmEmail;
        private readonly IForgotPassword _forgotPassword;
        private readonly ILogin _login;
        private readonly IResetPassword _resetPassword;
        private readonly ILogger<AuthController> _logger;
        /// <summary>
        /// Construtor do Controller de Autenticação.
        /// </summary>
        /// <param name="confirmEmail">Serviço de confirmação de e-mail.</param>
        /// <param name="forgotPassword">Serviço de solicitação de reset de senha.</param>
        /// <param name="login">Serviço de login.</param>
        /// <param name="resetPassword">Serviço de reset de senha.</param>
        /// <param name="logger">Serviço de log.</param>
        public AuthController(IConfirmEmail confirmEmail,
            IForgotPassword forgotPassword,
            ILogin login,
            IResetPassword resetPassword,
            ILogger<AuthController> logger)
        {
            _confirmEmail = confirmEmail;
            _forgotPassword = forgotPassword;
            _login = login;
            _resetPassword = resetPassword;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Realiza a confirmação do e-mail do usuário através do token de validação fornecido e do E-mail do usuário.
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <param name="token">Token de validação</param>
        /// <returns>Resultado da solicitação de validação</returns>
        /// <response code="200">E-mail confirmado com sucesso</response>
        /// <response code="400">Requisição incorreta.</response>
        [AllowAnonymous]
        [HttpPost("ConfirmEmail", Name = "ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> ConfirmEmail(string? email, string? token)
        {
            try
            {
                string result = await _confirmEmail.ExecuteAsync(email, token);
                _logger.LogInformation("Resultado: {Result}", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza a solicitação de reset de senha para o e-mail informado.
        /// Após a solicitação, um código é enviado para o e-mail do usuário.
        /// </summary>
        /// <param></param>
        /// <returns>Resultado da requisição</returns>
        /// <response code="200">Solicitação realizada com sucesso</response>
        /// <response code="400">Requisição incorreta.</response>
        [AllowAnonymous]
        [HttpPost("ForgotPassword", Name = "ForgotPassword")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> ForgotPassword(string? email)
        {
            try
            {
                string result = await _forgotPassword.ExecuteAsync(email);
                _logger.LogInformation("Resultado: {Result}", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza o login do usário.
        /// </summary>
        /// <param></param>
        /// <returns>Retorna token de acesso</returns>
        /// <response code="200">Retorna token de acesso</response>
        /// <response code="400">Requisição incorreta.</response>
        [AllowAnonymous]
        [HttpPost("Login", Name = "Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserLoginOutput>> Login([FromBody] UserLoginInput request)
        {
            try
            {
                var model = await _login.ExecuteAsync(request);
                _logger.LogInformation("Login realizado pelo usuário: {email}.", request.Email);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza a alteração de senha do usário.
        /// </summary>
        /// <param></param>
        /// <returns>Retorna o status da alteração</returns>
        /// <response code="200">Retorna o status da alteração</response>
        /// <response code="400">Requisição incorreta.</response>
        [AllowAnonymous]
        [HttpPost("ResetPassword", Name = "ResetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> ResetPassword([FromBody] UserResetPasswordInput request)
        {
            try
            {
                string result = await _resetPassword.ExecuteAsync(request);
                _logger.LogInformation("Resultado: {Result}", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}