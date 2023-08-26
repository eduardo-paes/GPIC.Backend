using Application.Interfaces.UseCases.Area;
using Application.Ports.Area;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Área.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AreaController : ControllerBase
    {
        #region Global Scope
        private readonly IGetAreaById _getById;
        private readonly IGetAreasByMainArea _getAreasByMainArea;
        private readonly ICreateArea _create;
        private readonly IUpdateArea _update;
        private readonly IDeleteArea _delete;
        private readonly ILogger<AreaController> _logger;
        /// <summary>
        /// Construtor do Controller de Área.
        /// </summary>
        /// <param name="getById">Serviço de obtenção de área pelo id.</param>
        /// <param name="getAreasByMainArea">Serviço de obtenção de todas as áreas ativas da área principal.</param>
        /// <param name="create">Serviço de criação de área.</param>
        /// <param name="update">Serviço de atualização de área.</param>
        /// <param name="delete">Serviço de remoção de área.</param>
        /// <param name="logger">Serviço de log.</param>
        public AreaController(IGetAreaById getById,
            IGetAreasByMainArea getAreasByMainArea,
            ICreateArea create,
            IUpdateArea update,
            IDeleteArea delete,
            ILogger<AreaController> logger)
        {
            _getById = getById;
            _getAreasByMainArea = getAreasByMainArea;
            _create = create;
            _update = update;
            _delete = delete;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Busca área pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Área correspondente</returns>
        /// <response code="200">Retorna área correspondente</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhuma área encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadAreaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetById(Guid? id)
        {
            _logger.LogInformation("Executando ({MethodName}) com os parâmetros: Id = {id}", nameof(GetById), id);

            if (id == null)
            {
                const string errorMessage = "O id informado não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var model = await _getById.ExecuteAsync(id);
                _logger.LogInformation("Método ({MethodName}) executado. Retorno: Id = {id}", nameof(GetById), id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas as áreas ativas pela área principal.
        /// </summary>
        /// <param></param>
        /// <returns>Todas as áreas ativas da área principal</returns>
        /// <response code="200">Retorna todas as áreas ativas da área principal</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhuma área encontrada.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResumedReadAreaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAreasByMainArea(Guid? mainAreaId, int skip = 0, int take = 50)
        {
            _logger.LogInformation("Executando método com os parâmetros: MainAreaId = {mainAreaId}, Skip = {skip}, Take = {take}", mainAreaId, skip, take);

            if (mainAreaId == null)
            {
                const string errorMessage = "O MainAreaId informado não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var models = await _getAreasByMainArea.ExecuteAsync(mainAreaId, skip, take);
                if (models == null || !models.Any())
                {
                    const string errorMessage = "Nenhuma Área encontrada.";
                    _logger.LogWarning(errorMessage);
                    return NotFound(errorMessage);
                }
                _logger.LogInformation("Método finalizado, retorno: Número de entidades = {count}", models.Count());
                return Ok(models);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cria área.
        /// </summary>
        /// <param></param>
        /// <returns>Área criada</returns>
        /// <response code="201">Retorna área criada</response>
        /// <response code="400">Requisição incorreta.</response>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailedReadAreaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Create([FromBody] CreateAreaInput request)
        {
            try
            {
                var createdArea = await _create.ExecuteAsync(request);
                _logger.LogInformation("Área criada {id}", createdArea?.Id);
                return CreatedAtAction(nameof(GetById), new { id = createdArea?.Id }, createdArea);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza área.
        /// </summary>
        /// <param></param>
        /// <returns>Área atualizada</returns>
        /// <response code="200">Retorna área atualizada</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadAreaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<IActionResult> Update(Guid? id, [FromBody] UpdateAreaInput request)
        {
            if (id == null)
            {
                return BadRequest("O id informado não pode ser nulo.");
            }

            try
            {
                var model = await _update.ExecuteAsync(id, request);
                _logger.LogInformation("Área atualizada: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove área.
        /// </summary>
        /// <param></param>
        /// <returns>Área removida</returns>
        /// <response code="200">Retorna área removida</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadAreaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("O id informado não pode ser nulo.");
            }

            try
            {
                var model = await _delete.ExecuteAsync(id.Value);
                _logger.LogInformation("Área removida: {id}", model?.Id);
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