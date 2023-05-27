using Adapters.Gateways.Area;
using Adapters.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    /// <summary>
    /// Controller de Área.
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class AreaController : ControllerBase
    {
        #region Global Scope
        private readonly IAreaPresenterController _service;
        private readonly ILogger<AreaController> _logger;
        /// <summary>
        /// Construtor do Controller de Área.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public AreaController(IAreaPresenterController service, ILogger<AreaController> logger)
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
        public async Task<ActionResult<DetailedReadAreaResponse>> GetById(Guid? id)
        {
            _logger.LogInformation("Executando ({MethodName}) com os parâmetros: Id = {id}", id);

            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.GetById(id);
                _logger.LogInformation("Método ({MethodName}) executado. Retorno: Id = {id}", id);
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadAreaResponse>>> GetAreasByMainArea(Guid? mainAreaId, int skip = 0, int take = 50)
        {
            _logger.LogInformation("Executando método com os parâmetros: MainAreaId = {mainAreaId}, Skip = {skip}, Take = {take}", mainAreaId, skip, take);

            if (mainAreaId == null)
            {
                const string msg = "O MainAreaId informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var models = await _service.GetAreasByMainArea(mainAreaId, skip, take);
                if (models == null)
                {
                    const string msg = "Nenhuma Área encontrada.";
                    _logger.LogWarning(msg);
                    return NotFound(msg);
                }
                int count = models.Count();
                _logger.LogInformation("Método finalizado, retorno: Número de entidades = {count}", count);
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
        /// <response code="200">Retorna área criada</response>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadAreaResponse>> Create([FromBody] CreateAreaRequest request)
        {
            try
            {
                var model = await _service.Create(request) as DetailedReadAreaResponse;
                _logger.LogInformation("Método finalizado, retorno: Id = {id}", model?.Id);
                return Ok(model);
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
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadAreaResponse>> Update(Guid? id, [FromBody] UpdateAreaRequest request)
        {
            try
            {
                var model = await _service.Update(id, request) as DetailedReadAreaResponse;
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
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadAreaResponse>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value) as DetailedReadAreaResponse;
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