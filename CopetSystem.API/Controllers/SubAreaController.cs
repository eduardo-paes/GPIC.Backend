using CopetSystem.Application.DTOs.SubArea;
using CopetSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CopetSystem.API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class SubAreaController : ControllerBase
    {
        private readonly ISubAreaService _service;
        private readonly ILogger<SubAreaController> _logger;
        public SubAreaController(ISubAreaService service, ILogger<SubAreaController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Busca sub área pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Sub Área correspondente</returns>
        /// <response code="200">Retorna sub área correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadSubAreaDTO>> GetById(Guid? id)
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
                _logger.LogInformation($"Sub Área encontrada para o id {id}.");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas as sub áreas ativas pela área.
        /// </summary>
        /// <param></param>
        /// <returns>Todas as sub áreas ativas da área</returns>
        /// <response code="200">Retorna todas as sub áreas ativas da área</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadSubAreaDTO>>> GetSubAreasByArea(Guid? areaId, int skip = 0, int take = 50)
        {
            if (areaId == null)
            {
                const string msg = "O AreadId informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            var models = await _service.GetSubAreasByArea(areaId, skip, take);
            if (models == null)
            {
                const string msg = "Nenhuma Sub Área encontrada.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation($"Sub Áreas encontradas: {models.Count()}");
            return Ok(models);
        }

        /// <summary>
        /// Cria sub área.
        /// </summary>
        /// <param></param>
        /// <returns>Sub Área criada</returns>
        /// <response code="200">Retorna sub área criada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadSubAreaDTO>> Create([FromBody] CreateSubAreaDTO dto)
        {
            try
            {
                var model = await _service.Create(dto);
                _logger.LogInformation($"Sub Área criada: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza sub área.
        /// </summary>
        /// <param></param>
        /// <returns>Sub Área atualizada</returns>
        /// <response code="200">Retorna sub área atualizada</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<DetailedReadSubAreaDTO>> Update(Guid? id, [FromBody] UpdateSubAreaDTO dto)
        {
            try
            {
                var model = await _service.Update(id, dto);
                _logger.LogInformation($"Sub Área atualizada: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove sub área.
        /// </summary>
        /// <param></param>
        /// <returns>Sub Área removida</returns>
        /// <response code="200">Retorna sub área removida</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetailedReadSubAreaDTO>> Delete(Guid? id)
        {
            if (id == null)
            {
                string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value);
                _logger.LogInformation($"Sub Área removida: {model.Id}");
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