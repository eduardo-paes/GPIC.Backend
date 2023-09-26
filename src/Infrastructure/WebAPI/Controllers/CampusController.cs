using Application.Ports.Campus;
using Application.Interfaces.UseCases.Campus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Campus.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CampusController : ControllerBase
    {
        #region Global Scope
        private readonly IGetCampusById _getById;
        private readonly IGetCampuses _getAll;
        private readonly ICreateCampus _create;
        private readonly IUpdateCampus _update;
        private readonly IDeleteCampus _delete;
        private readonly ILogger<CampusController> _logger;
        /// <summary>
        /// Construtor do Controller de Campus.
        /// </summary>
        /// <param name="getById">Serviço de obtenção de campus pelo id.</param>
        /// <param name="getAll">Serviço de obtenção de todos os campus ativos.</param>
        /// <param name="create">Serviço de criação de campus.</param>
        /// <param name="update">Serviço de atualização de campus.</param>
        /// <param name="delete">Serviço de remoção de campus.</param>
        /// <param name="logger">Serviço de log.</param>
        public CampusController(IGetCampusById getById,
            IGetCampuses getAll,
            ICreateCampus create,
            IUpdateCampus update,
            IDeleteCampus delete,
            ILogger<CampusController> logger)
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
        /// Busca campus pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Campus correspondente</returns>
        /// <response code="200">Retorna campus correspondente</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="404">Campus não encontrado.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadCampusOutput>> GetById(Guid? id)
        {
            try
            {
                var model = await _getById.ExecuteAsync(id);
                if (model == null)
                {
                    const string errorMessage = "Campus não encontrado.";
                    _logger.LogWarning(errorMessage);
                    return NotFound(errorMessage);
                }
                _logger.LogInformation("Campus encontrado para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas os campus ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todas os campus ativos</returns>
        /// <response code="200">Retorna todas os campus ativos</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="404">Nenhum Campus encontrado.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<ResumedReadCampusOutput>>> GetAll(int skip = 0, int take = 50)
        {
            try
            {
                var models = await _getAll.ExecuteAsync(skip, take);
                if (models == null)
                {
                    const string errorMessage = "Nenhum Campus encontrado.";
                    _logger.LogWarning(errorMessage);
                    return NotFound(errorMessage);
                }
                _logger.LogInformation("Campus encontrados: {quantidade}", models.Count());
                return Ok(models);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cria um novo campus.
        /// </summary>
        /// <param name="request">Informações do campus</param>
        /// <returns>O campus criado</returns>
        /// <response code="201">Retorna o campus recém-criado</response>
        /// <response code="400">Requisição inválida, se a entrada for inválida</response>
        /// <response code="401">Não autorizado, se o usuário não tiver a função necessária</response>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailedReadCampusOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadCampusOutput>> Create([FromBody] CreateCampusInput request)
        {
            try
            {
                var model = await _create.ExecuteAsync(request);
                _logger.LogInformation("Campus criado: {id}", model?.Id);
                return CreatedAtAction(nameof(GetById), new { id = model?.Id }, model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um campus.
        /// </summary>
        /// <param name="id">O ID do campus a ser atualizado</param>
        /// <param name="request">Informações de atualização do campus</param>
        /// <returns>O campus atualizado</returns>
        /// <response code="200">Retorna o campus atualizado</response>
        /// <response code="400">Requisição inválida, se a entrada for inválida</response>
        /// <response code="401">Não autorizado, se o usuário não tiver a função necessária</response>
        /// <response code="404">Não encontrado, se o campus não existir</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadCampusOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadCampusOutput>> Update(Guid? id, [FromBody] UpdateCampusInput request)
        {
            if (id == null)
            {
                return BadRequest("O ID do campus não foi fornecido.");
            }

            try
            {
                var model = await _update.ExecuteAsync(id.Value, request);

                if (model == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }

                _logger.LogInformation("Campus atualizado: {id}", model.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove um campus.
        /// </summary>
        /// <param name="id">O ID do campus a ser removido</param>
        /// <returns>O campus removido</returns>
        /// <response code="200">Retorna o campus removido</response>
        /// <response code="400">Requisição inválida, se o ID for nulo</response>
        /// <response code="401">Não autorizado, se o usuário não tiver a função necessária</response>
        /// <response code="404">Não encontrado, se o campus não existir</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadCampusOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadCampusOutput>> Remover(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do campus não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var model = await _delete.ExecuteAsync(id.Value);
                if (model == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }

                _logger.LogInformation("Campus removido: {id}", model.Id);
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