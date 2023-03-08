using CopetSystem.Application.DTOs.Area;
using CopetSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CopetSystem.API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _service;
        private readonly ILogger<AreaController> _logger;
        public AreaController(IAreaService service, ILogger<AreaController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Busca área pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Área correspondente</returns>
        /// <response code="200">Retorna área correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReadAreaDTO>> GetById(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.GetById(id);
                _logger.LogInformation($"Área encontrada para o id {id}.");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas as áreas ativas pela área principal.
        /// </summary>
        /// <param></param>
        /// <returns>Todas as áreas ativas da área principal</returns>
        /// <response code="200">Retorna todas as áreas ativas da área principal</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReadAreaDTO>>> GetAreasByMainArea(Guid? mainAreadId, int skip = 0, int take = 50)
        {
            if (mainAreadId == null)
            {
                const string msg = "O MainAreadId informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            var models = await _service.GetAreasByMainArea(mainAreadId, skip, take);
            if (models == null)
            {
                string msg = "Nenhuma Área encontrada.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation($"Áreas encontradas: {models.Count()}");
            return Ok(models);
        }

        /// <summary>
        /// Cria área.
        /// </summary>
        /// <param></param>
        /// <returns>Área criada</returns>
        /// <response code="200">Retorna área criada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReadAreaDTO>> Create([FromBody] CreateAreaDTO dto)
        {
            try
            {
                var model = await _service.Create(dto);
                _logger.LogInformation($"Área criada: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza área.
        /// </summary>
        /// <param></param>
        /// <returns>Área atualizada</returns>
        /// <response code="200">Retorna área atualizada</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadAreaDTO>> Update(Guid? id, [FromBody] UpdateAreaDTO dto)
        {
            try
            {
                var model = await _service.Update(id, dto);
                _logger.LogInformation($"Área atualizada: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove área.
        /// </summary>
        /// <param></param>
        /// <returns>Área removida</returns>
        /// <response code="200">Retorna área removida</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReadAreaDTO>> Delete(Guid? id)
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
                _logger.LogInformation($"Área removida: {model.Id}");
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