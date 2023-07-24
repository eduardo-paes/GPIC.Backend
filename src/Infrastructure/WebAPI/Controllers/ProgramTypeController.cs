using Adapters.Gateways.ProgramType;
using Adapters.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    /// <summary>
    /// Controller de Tipo de Programa.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProgramTypeController : ControllerBase
    {
        #region Global Scope
        private readonly IProgramTypePresenterController _service;
        private readonly ILogger<ProgramTypeController> _logger;
        /// <summary>
        /// Construtor do Controller de Tipo de Programa.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public ProgramTypeController(IProgramTypePresenterController service, ILogger<ProgramTypeController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca tipo de programa pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Tipo de Programa correspondente</returns>
        /// <response code="200">Retorna tipo de programa correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadProgramTypeResponse>> GetById(Guid? id)
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
                _logger.LogInformation("Tipo de Programa encontrado para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas os tipos de programas ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todas os tipos de programas ativos</returns>
        /// <response code="200">Retorna todas os tipos de programas ativos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadProgramTypeResponse>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _service.GetAll(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum Tipo de Programa encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Tipos de Programas encontrados: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Cria tipo de programa.
        /// </summary>
        /// <param></param>
        /// <returns>Tipo de Programa criado</returns>
        /// <response code="200">Retorna tipo de programa criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadProgramTypeResponse>> Create([FromBody] CreateProgramTypeRequest request)
        {
            try
            {
                var model = await _service.Create(request) as DetailedReadProgramTypeResponse;
                _logger.LogInformation("Tipo de Programa criado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza tipo de programa.
        /// </summary>
        /// <param></param>
        /// <returns>Tipo de Programa atualizado</returns>
        /// <response code="200">Retorna tipo de programa atualizado</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadProgramTypeResponse>> Update(Guid? id, [FromBody] UpdateProgramTypeRequest request)
        {
            try
            {
                var model = await _service.Update(id, request) as DetailedReadProgramTypeResponse;
                _logger.LogInformation("Tipo de Programa atualizado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove tipo de programa.
        /// </summary>
        /// <param></param>
        /// <returns>Tipo de Programa removido</returns>
        /// <response code="200">Retorna tipo de programa removido</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadProgramTypeResponse>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value) as DetailedReadProgramTypeResponse;
                _logger.LogInformation("Tipo de Programa removido: {id}", model?.Id);
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