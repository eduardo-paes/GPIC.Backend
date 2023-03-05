using System;
using CopetSystem.Application.DTOs.User;
using CopetSystem.Application.Interfaces;
using CopetSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CopetSystem.API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
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
            {
                string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.GetById(id);
                _logger.LogInformation($"Usuário encontrado para o id {id}.");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
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
            var models = await _service.GetActiveUsers();
            if (models == null)
            {
                string msg = "Nenhum usuário encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation($"Usuários encontrados: {models.Count()}");
            return Ok(models);
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
            var models = await _service.GetInactiveUsers();
            if (models == null)
            {
                string msg = "Nenhum usuário encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation($"Usuários encontrados: {models.Count()}");
            return Ok(models);
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
            try
            {
                var model = await _service.Update(id, dto);
                _logger.LogInformation($"Usuário atualizado: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
            {
                string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Activate(id.Value);
                _logger.LogInformation($"Usuário ativado: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
            {
                string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Deactivate(id.Value);
                _logger.LogInformation($"Usuário desativado: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}