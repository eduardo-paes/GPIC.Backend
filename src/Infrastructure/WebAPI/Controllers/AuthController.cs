using Adapters.DTOs.Auth;
using Adapters.Proxies.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Global Scope
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Realiza a confirmação do e-mail do usuário através do token de validação fornecido e do Id do usuário.
        /// </summary>
        /// <param name="userId">Id do usuário</param>
        /// <param name="token">Token de validação</param>
        /// <returns>Resultado da solicitação de validação</returns>
        /// <response code="200">E-mail confirmado com sucesso</response>
        [AllowAnonymous]
        [HttpPost("ConfirmEmail", Name = "ConfirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail(Guid? userId, string? token)
        {
            try
            {
                var result = await _authService.ConfirmEmail(userId, token);
                _logger.LogInformation(result);
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
        [AllowAnonymous]
        [HttpPost("ForgotPassword", Name = "ForgotPassword")]
        public async Task<ActionResult<string>> ForgotPassword(string? email)
        {
            try
            {
                var result = await _authService.ForgotPassword(email);
                _logger.LogInformation(result);
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
        [AllowAnonymous]
        [HttpPost("Login", Name = "Login")]
        public async Task<ActionResult<UserLoginResponseDTO>> Login([FromBody] UserLoginRequestDTO dto)
        {
            try
            {
                var model = await _authService.Login(dto);
                _logger.LogInformation($"Login realizado pelo usuário: {dto.Email}.");
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
        [AllowAnonymous]
        [HttpPost("ResetPassword", Name = "ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] UserResetPasswordDTO dto)
        {
            try
            {
                var res = await _authService.ResetPassword(dto);
                _logger.LogInformation(res);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}