using System;
using CopetSystem.Application.DTOs.Auth;
using CopetSystem.Application.DTOs.User;
using CopetSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CopetSystem.API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
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
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _service.Login(dto);
                    return Ok(user);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Campos inválidos.");
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
            if (ModelState.IsValid)
            {
                UserReadDTO? user;
                try
                {
                    user = await _service.Register(dto);
                    return Ok(user);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Campos inválidos.");
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
            if (ModelState.IsValid)
            {
                try
                {
                    var res = await _service.ResetPassword(dto);
                    return Ok(res);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Campos inválidos.");
            }
        }
    }
}

