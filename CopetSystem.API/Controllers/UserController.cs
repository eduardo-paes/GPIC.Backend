using System;
using CopetSystem.Application.DTOs;
using CopetSystem.Application.Interfaces;
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
        /// Busca todos os usuários ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todos os usuários ativos</returns>
        /// <response code="200">Retorna todos os usuários ativos</response>
        [HttpGet(Name = "GetAllActiveUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> Get()
        {
            var users = await _service.GetActiveUsers();
            if (users == null)
            {
                return NotFound("Nenhum usuário encontrado.");
            }
            return Ok(users);
        }
    }
}