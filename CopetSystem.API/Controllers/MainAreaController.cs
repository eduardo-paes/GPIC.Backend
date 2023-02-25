using System;
using CopetSystem.Application.DTOs.User;
using CopetSystem.Application.Interfaces;
using CopetSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CopetSystem.API.Queries
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Busca usuário pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Todos os usuários ativos</returns>
        /// <response code="200">Retorna todos os usuários ativos</response>
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetById(Guid? id)
        {
            if (id == null)
                return BadRequest("O id informado não pode ser nulo.");

            var users = await _service.GetById(id);
            if (users == null)
            {
                return NotFound("Nenhum usuário encontrado.");
            }
            return Ok(users);
        }

        /// <summary>
        /// Busca todos os usuários ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todos os usuários ativos</returns>
        /// <response code="200">Retorna todos os usuários ativos</response>
        [HttpGet("Active/", Name = "GetAllActiveUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllActive()
        {
            var users = await _service.GetActiveUsers();
            if (users == null)
            {
                return NotFound("Nenhum usuário encontrado.");
            }
            return Ok(users);
        }

        /// <summary>
        /// Busca todos os usuários inativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todos os usuários inativos</returns>
        /// <response code="200">Retorna todos os usuários ativos</response>
        [HttpGet("Inactive/", Name = "GetAllInactiveUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllInactive()
        {
            var users = await _service.GetInactiveUsers();
            if (users == null)
            {
                return NotFound("Nenhum usuário encontrado.");
            }
            return Ok(users);
        }

        /// <summary>
        /// Realiza atualização de usário.
        /// </summary>
        /// <param></param>
        /// <returns>Retorna usuário atualizado</returns>
        /// <response code="200">Retorna usuário atualizado</response>
        [HttpPut("{id}", Name = "UpdateUser")]
        public async Task<ActionResult<UserReadDTO>> Update(Guid? id, [FromBody] UserUpdateDTO dto)
        {
            UserReadDTO? user;
            try
            {
                user = await _service.Update(id, dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza reativação de usário.
        /// </summary>
        /// <param></param>
        /// <returns>Retorna usuário reativado</returns>
        /// <response code="200">Retorna usuário reativado</response>
        [HttpPut("Active/{id}", Name = "ActivateUser")]
        public async Task<ActionResult<UserReadDTO>> Activate(Guid? id)
        {
            if (id == null)
                return BadRequest("O id informado não pode ser nulo.");

            UserReadDTO? user;
            try
            {
                user = await _service.Activate(id.Value);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza desativação de usário.
        /// </summary>
        /// <param></param>
        /// <returns>Retorna usuário desativado</returns>
        /// <response code="200">Retorna usuário desativado</response>
        [HttpPut("Inactive/{id}", Name = "DeactivateUser")]
        public async Task<ActionResult<UserReadDTO>> Deactivate(Guid? id)
        {
            if (id == null)
                return BadRequest("O id informado não pode ser nulo.");

            UserReadDTO? user;
            try
            {
                user = await _service.Deactivate(id.Value);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}