using Adapters.DTOs.Campus;
using Adapters.Proxies.Campus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class CampusController : ControllerBase
    {
        #region Global Scope
        private readonly ICampusService _service;
        private readonly ILogger<CampusController> _logger;
        public CampusController(ICampusService service, ILogger<CampusController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca campus pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Campus correspondente</returns>
        /// <response code="200">Retorna campus correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadCampusDTO>> GetById(Guid? id)
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
                _logger.LogInformation($"Campus encontrado para o id {id}.");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas os campus ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todas os campus ativos</returns>
        /// <response code="200">Retorna todas os campus ativos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadCampusDTO>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _service.GetAll(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum Campus encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation($"Campus encontrados: {models.Count()}");
            return Ok(models);
        }

        /// <summary>
        /// Cria campus.
        /// </summary>
        /// <param></param>
        /// <returns>Campus criado</returns>
        /// <response code="200">Retorna campus criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadCampusDTO>> Create([FromBody] CreateCampusDTO dto)
        {
            try
            {
                var model = await _service.Create(dto) as DetailedReadCampusDTO;
                _logger.LogInformation($"Campus criado: {model?.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza campus.
        /// </summary>
        /// <param></param>
        /// <returns>Campus atualizado</returns>
        /// <response code="200">Retorna campus atualizado</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<DetailedReadCampusDTO>> Update(Guid? id, [FromBody] UpdateCampusDTO dto)
        {
            try
            {
                var model = await _service.Update(id, dto) as DetailedReadCampusDTO;
                _logger.LogInformation($"Campus atualizado: {model?.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove campus.
        /// </summary>
        /// <param></param>
        /// <returns>Campus removido</returns>
        /// <response code="200">Retorna campus removido</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetailedReadCampusDTO>> Delete(Guid? id)
        {
            if (id == null)
            {
                string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value) as DetailedReadCampusDTO;
                _logger.LogInformation($"Campus removido: {model?.Id}");
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