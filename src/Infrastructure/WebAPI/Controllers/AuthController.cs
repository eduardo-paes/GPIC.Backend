using Application.DTOs.Auth;
using Application.DTOs.User;
using Application.Proxies.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Global Scope
        private readonly ILoginUser _loginUser;
        private readonly IRegisterUser _registerUser;
        private readonly IResetPasswordUser _resetPasswordUser;
        private readonly ILogger<AuthController> _logger;
        public AuthController(ILoginUser loginUser,
                              IRegisterUser registerUser,
                              IResetPasswordUser resetPasswordUser,
                              ILogger<AuthController> logger)
        {
            _loginUser = loginUser;
            _registerUser = registerUser;
            _resetPasswordUser = resetPasswordUser;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Realiza o login do usário.
        /// </summary>
        /// <param></param>
        /// <returns>Retorna token de acesso</returns>
        /// <response code="200">Retorna token de acesso</response>
        [HttpPost("Login", Name = "LoginUser")]
        public async Task<ActionResult<UserLoginResponseDTO>> Login([FromBody] UserLoginRequestDTO dto)
        {
            try
            {
                var model = await _loginUser.Execute(dto);
                _logger.LogInformation($"Login realizado pelo usuário: {model.Id}.");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza criação de usário.
        /// </summary>
        /// <param></param>
        /// <returns>Retorna usuário criado</returns>
        /// <response code="200">Retorna usuário criado</response>
        [HttpPost("Register", Name = "RegisterUser")]
        public async Task<ActionResult<UserReadDTO>> Register([FromBody] UserRegisterDTO dto)
        {
            try
            {
                var model = await _registerUser.Execute(dto);
                _logger.LogInformation($"Usuário criado: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza a alteração de senha do usário.
        /// </summary>
        /// <param></param>
        /// <returns>Retorna o status da alteração</returns>
        /// <response code="200">Retorna o status da alteração</response>
        [HttpPost("ResetPassword", Name = "ResetPasswordUser")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] UserResetPasswordDTO dto)
        {
            try
            {
                var res = await _resetPasswordUser.Execute(dto);
                _logger.LogInformation($"Solicitação de reset de senha realizada para o usuário: {dto.Id}");
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}