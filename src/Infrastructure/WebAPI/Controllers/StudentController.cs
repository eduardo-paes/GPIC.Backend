using Adapters.Gateways.Student;
using Adapters.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Estudante.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class StudentController : ControllerBase
    {
        #region Global Scope
        private readonly IStudentPresenterController _service;
        private readonly ILogger<StudentController> _logger;
        /// <summary>
        /// Construtor do Controller de Estudante.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public StudentController(IStudentPresenterController service, ILogger<StudentController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Busca Estudante pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante correspondente</returns>
        /// <response code="200">Retorna Estudante correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadStudentResponse>> GetById(Guid? id)
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
                _logger.LogInformation("Estudante encontrado para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca Estudante pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante correspondente</returns>
        /// <response code="200">Retorna Estudante correspondente</response>
        [HttpGet("RegistrationCode/{registrationCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadStudentResponse>> GetByRegistrationCode(string? registrationCode)
        {
            if (registrationCode == null)
            {
                const string msg = "A matrícula informada não pode ser nula.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                Adapters.Gateways.Base.IResponse model = await _service.GetByRegistrationCode(registrationCode);
                _logger.LogInformation("Estudante encontrado para a matrícula {registrationCode}.", registrationCode);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Busca todas os Estudante ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todas os Estudante ativos</returns>
        /// <response code="200">Retorna todas os Estudante ativos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadStudentResponse>>> GetAll(int skip = 0, int take = 50)
        {
            IEnumerable<Adapters.Gateways.Base.IResponse> models = await _service.GetAll(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum Estudante encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Estudante encontrados: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Cria Estudante.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante criado</returns>
        /// <response code="200">Retorna Estudante criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<DetailedReadStudentResponse>> Create([FromBody] CreateStudentRequest request)
        {
            try
            {
                DetailedReadStudentResponse? model = await _service.Create(request) as DetailedReadStudentResponse;
                _logger.LogInformation("Estudante criado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza Estudante.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante atualizado</returns>
        /// <response code="200">Retorna Estudante atualizado</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN, STUDENT")]
        public async Task<ActionResult<DetailedReadStudentResponse>> Update(Guid? id, [FromBody] UpdateStudentRequest request)
        {
            try
            {
                DetailedReadStudentResponse? model = await _service.Update(id, request) as DetailedReadStudentResponse;
                _logger.LogInformation("Estudante atualizado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove Estudante.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante removido</returns>
        /// <response code="200">Retorna Estudante removido</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN, STUDENT")]
        public async Task<ActionResult<DetailedReadStudentResponse>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                DetailedReadStudentResponse? model = await _service.Delete(id.Value) as DetailedReadStudentResponse;
                _logger.LogInformation("Estudante removido: {id}", model?.Id);
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