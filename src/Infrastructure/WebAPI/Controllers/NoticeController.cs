using Adapters.Gateways.Notice;
using Adapters.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    /// <summary>
    /// Controller de Edital.
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class NoticeController : ControllerBase
    {
        #region Global Scope
        private readonly INoticePresenterController _service;
        private readonly ILogger<NoticeController> _logger;
        /// <summary>
        /// Construtor do Controller de Edital.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public NoticeController(INoticePresenterController service, ILogger<NoticeController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca edital pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Edital correspondente</returns>
        /// <response code="200">Retorna edital correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadNoticeResponse>> GetById(Guid? id)
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
                _logger.LogInformation("Edital encontrado para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas os editais ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todas os editais ativos</returns>
        /// <response code="200">Retorna todas os editais ativos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadNoticeResponse>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _service.GetAll(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum Edital encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Editais encontrados: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Cria edital.
        /// </summary>
        /// <param></param>
        /// <returns>Edital criado</returns>
        /// <response code="200">Retorna edital criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadNoticeResponse>> Create([FromForm] CreateNoticeRequest request)
        {
            try
            {
                var model = await _service.Create(request) as DetailedReadNoticeResponse;
                _logger.LogInformation("Edital criado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza edital.
        /// </summary>
        /// <param></param>
        /// <returns>Edital atualizado</returns>
        /// <response code="200">Retorna edital atualizado</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadNoticeResponse>> Update(Guid? id, [FromForm] UpdateNoticeRequest request)
        {
            try
            {
                var model = await _service.Update(id, request) as DetailedReadNoticeResponse;
                _logger.LogInformation("Edital atualizado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove edital.
        /// </summary>
        /// <param></param>
        /// <returns>Edital removido</returns>
        /// <response code="200">Retorna edital removido</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadNoticeResponse>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value) as DetailedReadNoticeResponse;
                _logger.LogInformation("Edital removido: {id}", model?.Id);
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