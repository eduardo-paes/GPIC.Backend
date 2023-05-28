using Adapters.Gateways.StudentAssistanceScholarship;
using Adapters.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    /// <summary>
    /// Controller de Bolsa de Assistência.
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class StudentAssistanceScholarshipController : ControllerBase
    {
        #region Global Scope
        private readonly IStudentAssistanceScholarshipPresenterController _service;
        private readonly ILogger<StudentAssistanceScholarshipController> _logger;
        /// <summary>
        /// Construtor do Controller de Bolsa de Assistência.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public StudentAssistanceScholarshipController(IStudentAssistanceScholarshipPresenterController service, ILogger<StudentAssistanceScholarshipController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca bolsa de assistência pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Bolsa de Assistência correspondente</returns>
        /// <response code="200">Retorna bolsa de assistência correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadStudentAssistanceScholarshipResponse>> GetById(Guid? id)
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
                _logger.LogInformation("Bolsa de Assistência encontrado para o id {id}.", id);
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadStudentAssistanceScholarshipResponse>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _service.GetAll(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum Bolsa de Assistência encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Tipos de Programas encontrados: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Cria bolsa de assistência.
        /// </summary>
        /// <param></param>
        /// <returns>Bolsa de Assistência criado</returns>
        /// <response code="200">Retorna bolsa de assistência criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadStudentAssistanceScholarshipResponse>> Create([FromBody] CreateStudentAssistanceScholarshipRequest request)
        {
            try
            {
                var model = await _service.Create(request) as DetailedReadStudentAssistanceScholarshipResponse;
                _logger.LogInformation("Bolsa de Assistência criado: {id}", model?.Id);
                return Ok(model);
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
        /// <returns>Bolsa de Assistência atualizado</returns>
        /// <response code="200">Retorna bolsa de assistência atualizado</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadStudentAssistanceScholarshipResponse>> Update(Guid? id, [FromBody] UpdateStudentAssistanceScholarshipRequest request)
        {
            try
            {
                var model = await _service.Update(id, request) as DetailedReadStudentAssistanceScholarshipResponse;
                _logger.LogInformation("Bolsa de Assistência atualizado: {id}", model?.Id);
                return Ok(model);
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
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadStudentAssistanceScholarshipResponse>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value) as DetailedReadStudentAssistanceScholarshipResponse;
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