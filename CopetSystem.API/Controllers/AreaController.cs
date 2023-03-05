using System;
using CopetSystem.Application.DTOs.Auth;
using CopetSystem.Application.DTOs.Area;
using CopetSystem.Application.DTOs.User;
using CopetSystem.Application.Interfaces;
using CopetSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CopetSystem.API.Queries
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _service;
        public AreaController(IAreaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Busca área principal pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Área principal correspondente</returns>
        /// <response code="200">Retorna área principal correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReadAreaDTO>> GetById(Guid? id)
        {
            if (id == null)
                return BadRequest("O id informado não pode ser nulo.");

            try
            {
                var models = await _service.GetById(id);
                return Ok(models);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas as áreas principais ativas.
        /// </summary>
        /// <param></param>
        /// <returns>Todas as áreas principais ativas</returns>
        /// <response code="200">Retorna todas as áreas principais ativas</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReadAreaDTO>>> GetAll()
        {
            var models = await _service.GetAll();
            if (models == null)
            {
                return NotFound("Nenhuma Área Principal encontrada.");
            }
            return Ok(models);
        }

        /// <summary>
        /// Cria área principal.
        /// </summary>
        /// <param></param>
        /// <returns>Área principal criada</returns>
        /// <response code="200">Retorna área principal criada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReadAreaDTO>> Create([FromBody] CreateAreaDTO dto)
        {
            if (ModelState.IsValid)
            {
                ReadAreaDTO? model;
                try
                {
                    model = await _service.Create(dto);
                    return Ok(model);
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
        /// Atualiza área principal.
        /// </summary>
        /// <param></param>
        /// <returns>Área principal atualizada</returns>
        /// <response code="200">Retorna área principal atualizada</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadAreaDTO>> Update(Guid? id, [FromBody] UpdateAreaDTO dto)
        {
            if (ModelState.IsValid)
            {
                ReadAreaDTO? model;
                try
                {
                    model = await _service.Update(id, dto);
                    return Ok(model);
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
        /// Remove área principal.
        /// </summary>
        /// <param></param>
        /// <returns>Área principal removida</returns>
        /// <response code="200">Retorna área principal removida</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReadAreaDTO>> Delete(Guid? id)
        {
            if (id == null)
                return BadRequest("O id informado não pode ser nulo.");

            ReadAreaDTO? model;
            try
            {
                model = await _service.Delete(id.Value);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}