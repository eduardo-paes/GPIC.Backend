using Adapters.Gateways.Professor;
using Adapters.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    /// <summary>
    /// Controller de Professor.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProfessorController : ControllerBase
    {
        #region Global Scope
        private readonly IProfessorPresenterController _service;
        private readonly ILogger<ProfessorController> _logger;
        /// <summary>
        /// Construtor do Controller de Professor.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public ProfessorController(IProfessorPresenterController service, ILogger<ProfessorController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca Professor pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Professor correspondente</returns>
        /// <response code="200">Retorna Professor correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadProfessorResponse>> GetById(Guid? id)
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
                _logger.LogInformation("Professor encontrado para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todos os Professor ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todos os Professor ativos</returns>
        /// <response code="200">Retorna todos os Professor ativos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadProfessorResponse>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _service.GetAll(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum Professor encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Professor encontrados: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Cria Professor.
        /// </summary>
        /// <param></param>
        /// <returns>Professor criado</returns>
        /// <response code="200">Retorna Professor criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<DetailedReadProfessorResponse>> Create([FromBody] CreateProfessorRequest request)
        {
            try
            {
                var model = await _service.Create(request) as DetailedReadProfessorResponse;
                _logger.LogInformation("Professor criado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza Professor.
        /// </summary>
        /// <param></param>
        /// <returns>Professor atualizado</returns>
        /// <response code="200">Retorna Professor atualizado</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<DetailedReadProfessorResponse>> Update(Guid? id, [FromBody] UpdateProfessorRequest request)
        {
            try
            {
                var model = await _service.Update(id, request) as DetailedReadProfessorResponse;
                _logger.LogInformation("Professor atualizado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove Professor.
        /// </summary>
        /// <param></param>
        /// <returns>Professor removido</returns>
        /// <response code="200">Retorna Professor removido</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<DetailedReadProfessorResponse>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value) as DetailedReadProfessorResponse;
                _logger.LogInformation("Professor removido: {id}", model?.Id);
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