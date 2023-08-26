using Application.Ports.Notice;
using Application.Interfaces.UseCases.Notice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Edital.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class NoticeController : ControllerBase
    {
        #region Global Scope
        private readonly IGetNoticeById _getById;
        private readonly IGetNotices _getAll;
        private readonly ICreateNotice _create;
        private readonly IUpdateNotice _update;
        private readonly IDeleteNotice _delete;
        private readonly ILogger<NoticeController> _logger;
        /// <summary>
        /// Construtor do Controller de Edital.
        /// </summary>
        /// <param name="getById">Serviço de obtenção de edital pelo id.</param>
        /// <param name="getAll">Serviço de obtenção de todos os editais ativos.</param>
        /// <param name="create">Serviço de criação de edital.</param>
        /// <param name="update">Serviço de atualização de edital.</param>
        /// <param name="delete">Serviço de remoção de edital.</param>
        /// <param name="logger">Serviço de log.</param>
        public NoticeController(IGetNoticeById getById,
            IGetNotices getAll,
            ICreateNotice create,
            IUpdateNotice update,
            IDeleteNotice delete,
            ILogger<NoticeController> logger)
        {
            _getById = getById;
            _getAll = getAll;
            _create = create;
            _update = update;
            _delete = delete;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Busca edital pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Edital correspondente</returns>
        /// <response code="200">Retorna edital correspondente</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Edital não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadNoticeOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadNoticeOutput>> GetById(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do edital não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var notice = await _getById.ExecuteAsync(id.Value);
                if (notice == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Edital encontrado para o ID {id}.", id);
                return Ok(notice);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas os editais ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todas os editais ativos</returns>
        /// <response code="200">Retorna todas os editais ativos</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhum edital encontrado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResumedReadNoticeOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<ResumedReadNoticeOutput>>> GetAll(int skip = 0, int take = 50)
        {
            var notices = await _getAll.ExecuteAsync(skip, take);
            if (notices == null || !notices.Any())
            {
                const string errorMessage = "Nenhum edital encontrado.";
                _logger.LogWarning(errorMessage);
                return NotFound(errorMessage);
            }
            _logger.LogInformation("Editais encontrados: {quantidade}", notices.Count());
            return Ok(notices);
        }

        /// <summary>
        /// Cria edital.
        /// </summary>
        /// <param></param>
        /// <returns>Edital criado</returns>
        /// <response code="201">Retorna edital criado</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailedReadNoticeOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadNoticeOutput>> Create([FromForm] CreateNoticeInput request)
        {
            try
            {
                var createdNotice = await _create.ExecuteAsync(request);
                _logger.LogInformation("Edital criado: {id}", createdNotice?.Id);
                return CreatedAtAction(nameof(GetById), new { id = createdNotice?.Id }, createdNotice);
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
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Edital não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadNoticeOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadNoticeOutput>> Update(Guid? id, [FromForm] UpdateNoticeInput request)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do edital não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var updatedNotice = await _update.ExecuteAsync(id.Value, request);
                if (updatedNotice == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Edital atualizado: {id}", updatedNotice?.Id);
                return Ok(updatedNotice);
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
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Edital não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadNoticeOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadNoticeOutput>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do edital não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var deletedNotice = await _delete.ExecuteAsync(id.Value);
                if (deletedNotice == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Edital removido: {id}", deletedNotice?.Id);
                return Ok(deletedNotice);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}