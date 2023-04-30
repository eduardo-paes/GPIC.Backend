using Adapters.DTOs.Notice;
using Adapters.Proxies.Notice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class NoticeController : ControllerBase
    {
        #region Global Scope
        private readonly INoticeService _service;
        private readonly ILogger<NoticeController> _logger;
        public NoticeController(INoticeService service, ILogger<NoticeController> logger)
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
        public async Task<ActionResult<DetailedReadNoticeDTO>> GetById(Guid? id)
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
                _logger.LogInformation($"Edital encontrado para o id {id}.");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
        public async Task<ActionResult<IEnumerable<ResumedReadNoticeDTO>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _service.GetAll(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum Edital encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation($"Editais encontrados: {models.Count()}");
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
        public async Task<ActionResult<DetailedReadNoticeDTO>> Create([FromForm] CreateNoticeDTO dto)
        {
            try
            {
                var model = await _service.Create(dto) as DetailedReadNoticeDTO;
                _logger.LogInformation($"Edital criado: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
        public async Task<ActionResult<DetailedReadNoticeDTO>> Update(Guid? id, [FromForm] UpdateNoticeDTO dto)
        {
            try
            {
                var model = await _service.Update(id, dto) as DetailedReadNoticeDTO;
                _logger.LogInformation($"Edital atualizado: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
        public async Task<ActionResult<DetailedReadNoticeDTO>> Delete(Guid? id)
        {
            if (id == null)
            {
                string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value) as DetailedReadNoticeDTO;
                _logger.LogInformation($"Edital removido: {model.Id}");
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