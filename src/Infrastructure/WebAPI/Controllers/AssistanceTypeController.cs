using Application.Ports.AssistanceType;
using Application.Interfaces.UseCases.AssistanceType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Bolsa de Assistência.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AssistanceTypeController : ControllerBase
    {
        #region Global Scope
        private readonly IGetAssistanceTypeById _getById;
        private readonly IGetAssistanceTypes _getAll;
        private readonly ICreateAssistanceType _create;
        private readonly IUpdateAssistanceType _update;
        private readonly IDeleteAssistanceType _delete;
        private readonly ILogger<AssistanceTypeController> _logger;
        /// <summary>
        /// Construtor do Controller de Bolsa de Assistência.
        /// </summary>
        /// <param name="getById">Serviço de obtenção de bolsa de assistência pelo id.</param>
        /// <param name="getAll">Serviço de obtenção de todas as bolsas de assistência ativas.</param>
        /// <param name="create">Serviço de criação de bolsa de assistência.</param>
        /// <param name="update">Serviço de atualização de bolsa de assistência.</param>
        /// <param name="delete">Serviço de remoção de bolsa de assistência.</param>
        /// <param name="logger">Serviço de log.</param>
        public AssistanceTypeController(IGetAssistanceTypeById getById,
            IGetAssistanceTypes getAll,
            ICreateAssistanceType create,
            IUpdateAssistanceType update,
            IDeleteAssistanceType delete,
            ILogger<AssistanceTypeController> logger)
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
        /// Busca bolsa de assistência pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Bolsa de Assistência correspondente</returns>
        /// <response code="200">Retorna bolsa de assistência correspondente</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhuma bolsa de assistência encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadAssistanceTypeOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O id informado não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var model = await _getById.ExecuteAsync(id);
                _logger.LogInformation("Bolsa de Assistência encontrada para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas as bolsas de assitência ativas.
        /// </summary>
        /// <param></param>
        /// <returns>Todas as bolsas de assitência ativas</returns>
        /// <response code="200">Retorna todas as bolsas de assitência ativas</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhuma bolsa de assitência encontrada.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ResumedReadAssistanceTypeOutput>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _getAll.ExecuteAsync(skip, take);
            if (models == null)
            {
                const string errorMessage = "Nenhum Bolsa de Assistência encontrada.";
                _logger.LogWarning(errorMessage);
                return NotFound(errorMessage);
            }
            _logger.LogInformation("Bolsas de Assistência encontradas: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Cria bolsa de assistência.
        /// </summary>
        /// <param></param>
        /// <returns>Bolsa de Assistência criada</returns>
        /// <response code="201">Retorna bolsa de assistência criada</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailedReadAssistanceTypeOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateAssistanceTypeInput request)
        {
            try
            {
                var createdAssistanceType = await _create.ExecuteAsync(request);
                _logger.LogInformation("Bolsa de Assistência criada {id}", createdAssistanceType?.Id);
                return CreatedAtAction(nameof(GetById), new { id = createdAssistanceType?.Id }, createdAssistanceType);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza bolsa de assistência.
        /// </summary>
        /// <param></param>
        /// <returns>Bolsa de Assistência atualizada</returns>
        /// <response code="200">Retorna bolsa de assistência atualizada</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadAssistanceTypeOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(Guid? id, [FromBody] UpdateAssistanceTypeInput request)
        {
            if (id == null)
            {
                return BadRequest("O id informado não pode ser nulo.");
            }

            try
            {
                var updatedAssistanceType = await _update.ExecuteAsync(id, request);
                _logger.LogInformation("Bolsa de Assistência atualizado: {id}", updatedAssistanceType?.Id);
                return Ok(updatedAssistanceType);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove bolsa de assistência.
        /// </summary>
        /// <param></param>
        /// <returns>Bolsa de Assistência removido</returns>
        /// <response code="200">Retorna bolsa de assistência removido</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadAssistanceTypeOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O id informado não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var model = await _delete.ExecuteAsync(id.Value);
                _logger.LogInformation("Bolsa de Assistência removido: {id}", model?.Id);
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