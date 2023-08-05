using Application.Ports.StudentDocuments;
using Application.Interfaces.UseCases.StudentDocuments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Documentos de Estudante.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class StudentDocumentsController : ControllerBase
    {
        #region Global Scope
        private readonly IGetStudentDocumentsByProjectId _getStudentDocumentsByProjectId;
        private readonly IGetStudentDocumentsByStudentId _getStudentDocumentsByStudentId;
        private readonly ICreateStudentDocuments _createStudentDocuments;
        private readonly IUpdateStudentDocuments _updateStudentDocuments;
        private readonly IDeleteStudentDocuments _deleteStudentDocuments;
        private readonly ILogger<StudentDocumentsController> _logger;

        /// <summary>
        /// Construtor do Controller de Documentos de Estudante.
        /// </summary>
        /// <param name="getStudentDocumentsByProjectId">Use Case de busca de documentos de estudante pelo id do projeto.</param>
        /// <param name="getStudentDocumentsByStudentId">Use Case de busca de documentos de estudante pelo id do estudante.</param>
        /// <param name="createStudentDocuments">Use Case de adição de documentos de estudante.</param>
        /// <param name="updateStudentDocuments">Use Case de atualização de documentos de estudante.</param>
        /// <param name="deleteStudentDocuments">Use Case de remoção de documentos de estudante.</param>
        /// <param name="logger">Logger</param>
        public StudentDocumentsController(IGetStudentDocumentsByProjectId getStudentDocumentsByProjectId,
            IGetStudentDocumentsByStudentId getStudentDocumentsByStudentId,
            ICreateStudentDocuments createStudentDocuments,
            IUpdateStudentDocuments updateStudentDocuments,
            IDeleteStudentDocuments deleteStudentDocuments,
            ILogger<StudentDocumentsController> logger)
        {
            _getStudentDocumentsByProjectId = getStudentDocumentsByProjectId;
            _getStudentDocumentsByStudentId = getStudentDocumentsByStudentId;
            _createStudentDocuments = createStudentDocuments;
            _updateStudentDocuments = updateStudentDocuments;
            _deleteStudentDocuments = deleteStudentDocuments;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Busca documentos de estudante pelo id do projeto.
        /// </summary>
        /// <param name="projectId">Id do projeto</param>
        /// <response code="200">Documentos de estudante encontrados.</response>
        /// <response code="400">O id informado não pode ser nulo.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="404">Documentos de estudante não encontrados.</response>
        [HttpGet("project/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadStudentDocumentsOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetailedReadStudentDocumentsOutput>> GetByProjectId(Guid? projectId)
        {
            if (projectId == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _getStudentDocumentsByProjectId.ExecuteAsync(projectId);
                if (model == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Documentos encontrados para o projeto com ID {projectId}.", projectId);
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
        /// <response code="200">Documentos de estudante encontrados.</response>
        /// <response code="400">O id informado não pode ser nulo.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="404">Documentos de estudante não encontrados.</response>
        [HttpGet("student/{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadStudentDocumentsOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetailedReadStudentDocumentsOutput>> GetByStudentId(Guid? studentId)
        {
            if (studentId == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _getStudentDocumentsByStudentId.ExecuteAsync(studentId);
                if (model == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Documentos encontrados para o estudante com ID {studentId}.", studentId);
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
        /// <response code="201">Documentos de estudante adicionados.</response>
        /// <response code="400">O id informado não pode ser nulo.</response>
        /// <response code="401">Usuário não autenticado.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailedReadStudentDocumentsOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "ADMIN, STUDENT")]
        public async Task<ActionResult<DetailedReadStudentDocumentsOutput>> Create([FromBody] CreateStudentDocumentsInput request)
        {
            try
            {
                var model = await _createStudentDocuments.ExecuteAsync(request);
                _logger.LogInformation("Documentos do estudante adicionados: {id}", model?.Id);
                return CreatedAtAction(nameof(GetByProjectId), new { projectId = model?.ProjectId }, model);
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
        /// <response code="200">Documentos de estudante atualizados.</response>
        /// <response code="400">O id informado não pode ser nulo.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="404">Documentos de estudante não encontrados.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadStudentDocumentsOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "ADMIN, STUDENT")]
        public async Task<ActionResult<DetailedReadStudentDocumentsOutput>> Update(Guid? id, [FromBody] UpdateStudentDocumentsInput request)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _updateStudentDocuments.ExecuteAsync(id.Value, request);
                if (model == null)
                {
                    return NotFound();
                }
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
        /// <response code="200">Documentos de estudante removidos.</response>
        /// <response code="400">O id informado não pode ser nulo.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="404">Documentos de estudante não encontrados.</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadStudentDocumentsOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "ADMIN, STUDENT")]
        public async Task<ActionResult<DetailedReadStudentDocumentsOutput>> Delete(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _deleteStudentDocuments.ExecuteAsync(id.Value);
                if (model == null)
                {
                    return NotFound();
                }
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