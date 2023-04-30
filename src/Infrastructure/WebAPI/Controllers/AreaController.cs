using Adapters.DTOs.Area;
using Adapters.Proxies.Area;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    /// <summary>
    /// Controller das entidades de área.
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class AreaController : ControllerBase
    {
        #region Global Scope
        private readonly IAreaService _service;
        private readonly ILogger<AreaController> _logger;
        public AreaController(IAreaService service, ILogger<AreaController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca área pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Área correspondente</returns>
        /// <response code="200">Retorna área correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadAreaDTO>> GetById(Guid? id)
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
        public async Task<ActionResult<IEnumerable<ResumedReadAreaDTO>>> GetAreasByMainArea(Guid? mainAreaId, int skip = 0, int take = 50)
        {
            if (mainAreaId == null)
            {
                const string msg = "O MainAreaId informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            var models = await _service.GetAreasByMainArea(mainAreaId, skip, take);
            if (models == null)
            {
                const string msg = "Nenhuma Área encontrada.";
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
        public async Task<ActionResult<DetailedReadAreaDTO>> Create([FromBody] CreateAreaDTO dto)
        {
            try
            {
                var model = await _service.Create(dto) as DetailedReadAreaDTO;
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
        public async Task<ActionResult<DetailedReadAreaDTO>> Update(Guid? id, [FromBody] UpdateAreaDTO dto)
        {
            try
            {
                var model = await _service.Update(id, dto) as DetailedReadAreaDTO;
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
        public async Task<ActionResult<DetailedReadAreaDTO>> Delete(Guid? id)
        {
            if (id == null)
            {
                string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value) as DetailedReadAreaDTO;
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