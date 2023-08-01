using Adapters.Gateways.SubArea;
using Adapters.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Sub Área.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class SubAreaController : ControllerBase
    {
        #region Global Scope
        private readonly ISubAreaPresenterController _service;
        private readonly ILogger<SubAreaController> _logger;
        /// <summary>
        /// Construtor do Controller de Sub Área.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public SubAreaController(ISubAreaPresenterController service, ILogger<SubAreaController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Busca sub área pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Sub Área correspondente</returns>
        /// <response code="200">Retorna sub área correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadSubAreaResponse>> GetById(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                Adapters.Gateways.Base.IResponse model = await _service.GetById(id);
                _logger.LogInformation("Sub Área encontrada para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas as sub áreas ativas pela área.
        /// </summary>
        /// <param></param>
        /// <returns>Todas as sub áreas ativas da área</returns>
        /// <response code="200">Retorna todas as sub áreas ativas da área</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadSubAreaResponse>>> GetSubAreasByArea(Guid? areaId, int skip = 0, int take = 50)
        {
            if (areaId == null)
            {
                const string msg = "O AreadId informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            IEnumerable<Adapters.Gateways.Base.IResponse> models = await _service.GetSubAreasByArea(areaId, skip, take);
            if (models == null)
            {
                const string msg = "Nenhuma Sub Área encontrada.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Sub Áreas encontradas: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Cria sub área.
        /// </summary>
        /// <param></param>
        /// <returns>Sub Área criada</returns>
        /// <response code="200">Retorna sub área criada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadSubAreaResponse>> Create([FromBody] CreateSubAreaRequest request)
        {
            try
            {
                DetailedReadSubAreaResponse? model = await _service.Create(request) as DetailedReadSubAreaResponse;
                _logger.LogInformation("Sub Área criada: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza sub área.
        /// </summary>
        /// <param></param>
        /// <returns>Sub Área atualizada</returns>
        /// <response code="200">Retorna sub área atualizada</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadSubAreaResponse>> Update(Guid? id, [FromBody] UpdateSubAreaRequest request)
        {
            try
            {
                DetailedReadSubAreaResponse? model = await _service.Update(id, request) as DetailedReadSubAreaResponse;
                _logger.LogInformation("Sub Área atualizada: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove sub área.
        /// </summary>
        /// <param></param>
        /// <returns>Sub Área removida</returns>
        /// <response code="200">Retorna sub área removida</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadSubAreaResponse>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                DetailedReadSubAreaResponse? model = await _service.Delete(id.Value) as DetailedReadSubAreaResponse;
                _logger.LogInformation("Sub Área removida: {id}", model?.Id);
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