using Adapters.Gateways.StudentDocuments;
using Adapters.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Documentos de Estudante.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StudentDocumentsController : ControllerBase
    {
        private readonly ILogger<StudentDocumentsController> _logger;
        private readonly IStudentDocumentsPresenterController _presenterController;
        /// <summary>
        /// Construtor do Controller de Documentos de Estudante.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="presenterController"></param>
        public StudentDocumentsController(ILogger<StudentDocumentsController> logger, IStudentDocumentsPresenterController presenterController)
        {
            _logger = logger;
            _presenterController = presenterController;
        }

        /// <summary>
        /// Busca documentos de estudante pelo id do projeto.
        /// </summary>
        /// <param name="projectId">Id do projeto</param>
        [HttpGet("project/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadStudentDocumentsResponse>> GetByProjectId(Guid? projectId)
        {
            if (projectId == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                Adapters.Gateways.Base.IResponse model = await _presenterController.GetByProjectId(projectId);
                _logger.LogInformation("Documentos encontrados para o projectId {projectId}.", projectId);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca documentos de estudante pelo id do estudante.
        /// </summary>
        /// <param name="studentId">Id do estudante</param>
        [HttpGet("student/{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadStudentDocumentsResponse>> GetByStudentId(Guid? studentId)
        {
            if (studentId == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                Adapters.Gateways.Base.IResponse model = await _presenterController.GetByStudentId(studentId);
                _logger.LogInformation("Documentos encontrados para o studentId {studentId}.", studentId);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Adiciona documentos de estudante ao projeto.
        /// </summary>
        /// <param name="request">Dados para adição dos documentos.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "ADMIN, STUDENT")]
        public async Task<ActionResult<DetailedReadStudentDocumentsResponse>> Create([FromBody] CreateStudentDocumentsRequest request)
        {
            try
            {
                DetailedReadStudentDocumentsResponse? model = await _presenterController.Create(request);
                _logger.LogInformation("Documentos do estudante adicionados: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza documentos de estudante.
        /// </summary>
        /// <param name="id">Id dos documentos do estudante</param>
        /// <param name="request">Dados para atualização dos documentos.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "ADMIN, STUDENT")]
        public async Task<ActionResult<DetailedReadStudentDocumentsResponse>> Update(Guid? id, [FromBody] UpdateStudentDocumentsRequest request)
        {
            try
            {
                DetailedReadStudentDocumentsResponse? model = await _presenterController.Update(id, request);
                _logger.LogInformation("Documentos do estudante atualizados: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove documentos de estudante.
        /// </summary>
        /// <param name="id">Id dos documentos do estudante</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN, STUDENT")]
        public async Task<ActionResult<DetailedReadStudentDocumentsResponse>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                DetailedReadStudentDocumentsResponse? model = await _presenterController.Delete(id.Value);
                _logger.LogInformation("Documentos do estudante removidos: {id}", model?.Id);
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