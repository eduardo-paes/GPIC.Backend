using Adapters.Gateways.Campus;
using Adapters.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    /// <summary>
    /// Controller de Campus.
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class CampusController : ControllerBase
    {
        #region Global Scope
        private readonly ICampusPresenterController _service;
        private readonly ILogger<CampusController> _logger;
        /// <summary>
        /// Construtor do Controller de Campus.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public CampusController(ICampusPresenterController service, ILogger<CampusController> logger)
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
        public async Task<ActionResult<DetailedReadCampusResponse>> GetById(Guid? id)
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
                _logger.LogInformation("Campus encontrado para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
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
        public async Task<ActionResult<IEnumerable<ResumedReadCampusResponse>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _service.GetAll(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum Campus encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Campus encontrados: {quantidade}", models.Count());
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
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadCampusResponse>> Create([FromBody] CreateCampusRequest request)
        {
            try
            {
                var model = await _service.Create(request) as DetailedReadCampusResponse;
                _logger.LogInformation("Campus criado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
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
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadCampusResponse>> Update(Guid? id, [FromBody] UpdateCampusRequest request)
        {
            try
            {
                var model = await _service.Update(id, request) as DetailedReadCampusResponse;
                _logger.LogInformation("Campus atualizado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
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
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadCampusResponse>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value) as DetailedReadCampusResponse;
                _logger.LogInformation("Campus removido: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}