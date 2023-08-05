using Application.Ports.MainArea;
using Application.Interfaces.UseCases.MainArea;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Área Principal.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class MainAreaController : ControllerBase
    {
        #region Global Scope
        private readonly IGetMainAreaById _getById;
        private readonly IGetMainAreas _getAll;
        private readonly ICreateMainArea _create;
        private readonly IUpdateMainArea _update;
        private readonly IDeleteMainArea _delete;
        private readonly ILogger<MainAreaController> _logger;
        /// <summary>
        /// Construtor do Controller de Área Principal.
        /// </summary>
        /// <param name="getById">Serviço de obtenção de área principal pelo id.</param>
        /// <param name="getAll">Serviço de obtenção de todas as áreas principais ativas.</param>
        /// <param name="create">Serviço de criação de área principal.</param>
        /// <param name="update">Serviço de atualização de área principal.</param>
        /// <param name="delete">Serviço de remoção de área principal.</param>
        /// <param name="logger">Serviço de log.</param>
        public MainAreaController(IGetMainAreaById getById,
            IGetMainAreas getAll,
            ICreateMainArea create,
            IUpdateMainArea update,
            IDeleteMainArea delete,
            ILogger<MainAreaController> logger)
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
        /// Busca área principal pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Área principal correspondente</returns>
        /// <response code="200">Retorna área principal correspondente</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Área principal não encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedMainAreaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetailedMainAreaOutput>> GetById(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O ID da área principal não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var mainArea = await _getById.ExecuteAsync(id.Value);
                if (mainArea == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Área Principal encontrada para o ID {id}.", id);
                return Ok(mainArea);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas as áreas principais ativas.
        /// </summary>
        /// <param></param>
        /// <returns>Todas as áreas principais ativas</returns>
        /// <response code="200">Retorna todas as áreas principais ativas</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhuma área principal encontrada.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResumedReadMainAreaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ResumedReadMainAreaOutput>>> GetAll(int skip = 0, int take = 50)
        {
            var mainAreas = await _getAll.ExecuteAsync(skip, take);
            if (mainAreas == null || !mainAreas.Any())
            {
                const string errorMessage = "Nenhuma Área Principal encontrada.";
                _logger.LogWarning(errorMessage);
                return NotFound(errorMessage);
            }
            _logger.LogInformation("Áreas principais encontradas: {quantidade}", mainAreas.Count());
            return Ok(mainAreas);
        }

        /// <summary>
        /// Cria área principal.
        /// </summary>
        /// <param></param>
        /// <returns>Área principal criada</returns>
        /// <response code="201">Retorna área principal criada</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailedMainAreaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedMainAreaOutput>> Create([FromBody] CreateMainAreaInput request)
        {
            try
            {
                var createdMainArea = await _create.ExecuteAsync(request);
                _logger.LogInformation("Área principal criada: {id}", createdMainArea?.Id);
                return CreatedAtAction(nameof(GetById), new { id = createdMainArea?.Id }, createdMainArea);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza área principal.
        /// </summary>
        /// <param></param>
        /// <returns>Área principal atualizada</returns>
        /// <response code="200">Retorna área principal atualizada</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Área principal não encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedMainAreaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedMainAreaOutput>> Update(Guid? id, [FromBody] UpdateMainAreaInput request)
        {
            if (id == null)
            {
                const string errorMessage = "O ID da área principal não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var updatedMainArea = await _update.ExecuteAsync(id.Value, request);
                if (updatedMainArea == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Área principal atualizada: {id}", updatedMainArea?.Id);
                return Ok(updatedMainArea);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove área principal.
        /// </summary>
        /// <param></param>
        /// <returns>Área principal removida</returns>
        /// <response code="200">Retorna área principal removida</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Área principal não encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedMainAreaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedMainAreaOutput>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O ID da área principal não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var deletedMainArea = await _delete.ExecuteAsync(id.Value);
                if (deletedMainArea == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Área principal removida: {id}", deletedMainArea?.Id);
                return Ok(deletedMainArea);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}