using System;
using CopetSystem.Application.DTOs;
using CopetSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CopetSystem.API.Queries
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
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