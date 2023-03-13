using Application.DTOs.Auth;
using Application.DTOs.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService service, ILogger<AuthController> logger)
        {
            _service = service;
            _logger = logger;
        }

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
                var model = await _service.Login(dto);
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
        public async Task<ActionResult<UserReadDTO>> Create([FromBody] UserRegisterDTO dto)
        {
            try
            {
                var model = await _service.Register(dto);
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
                var res = await _service.ResetPassword(dto);
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

