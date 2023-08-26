using Application.Ports.ProjectFinalReport;
using Application.Interfaces.UseCases.ProjectFinalReport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Relatório de Projeto.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProjectFinalReportController : ControllerBase
    {
        #region Global Scope
        private readonly IGetProjectFinalReportById _getProjectFinalReportById;
        private readonly IGetProjectFinalReportByProjectId _getProjectFinalReportByProjectId;
        private readonly ICreateProjectFinalReport _createProjectFinalReport;
        private readonly IUpdateProjectFinalReport _updateProjectFinalReport;
        private readonly IDeleteProjectFinalReport _deleteProjectFinalReport;
        private readonly ILogger<ProjectFinalReportController> _logger;

        /// <summary>
        /// Construtor do Controller de Relatório de Projeto.
        /// </summary>
        /// <param name="getProjectFinalReportById">Use Case de busca de relatório final de projeto por Id.</param>
        /// <param name="getProjectFinalReportByProjectId">Use Case de busca de relatório final de projeto por Id do projeto.</param>
        /// <param name="createProjectFinalReport">Use Case de criação de relatório final de projeto.</param>
        /// <param name="updateProjectFinalReport">Use Case de atualização de relatório final de projeto.</param>
        /// <param name="deleteProjectFinalReport">Use Case de remoção de relatório final de projeto.</param>
        /// <param name="logger">Logger.</param>
        public ProjectFinalReportController(IGetProjectFinalReportById getProjectFinalReportById,
            IGetProjectFinalReportByProjectId getProjectFinalReportByProjectId,
            ICreateProjectFinalReport createProjectFinalReport,
            IUpdateProjectFinalReport updateProjectFinalReport,
            IDeleteProjectFinalReport deleteProjectFinalReport,
            ILogger<ProjectFinalReportController> logger)
        {
            _getProjectFinalReportById = getProjectFinalReportById;
            _getProjectFinalReportByProjectId = getProjectFinalReportByProjectId;
            _createProjectFinalReport = createProjectFinalReport;
            _updateProjectFinalReport = updateProjectFinalReport;
            _deleteProjectFinalReport = deleteProjectFinalReport;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Busca relatório final de projeto pelo Id.
        /// </summary>
        /// <param name="id">Id do relatório final de projeto.</param>
        /// <returns>Relatório de projeto.</returns>
        /// <response code="200">Relatório de projeto encontrado.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Relatório de projeto não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DetailedReadProjectFinalReportOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadProjectFinalReportOutput>> GetById(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _getProjectFinalReportById.ExecuteAsync(id);
                if (model == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Relatório de Projeto encontrado para o ID {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca relatório final de projeto pelo Id do projeto.
        /// </summary>
        /// <param name="projectId">Id do projeto.</param>
        /// <returns>Relatório final do projeto.</returns>
        /// <response code="200">Relatórios de projeto encontrados.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Relatórios de projeto não encontrados.</response>
        [HttpGet("project/{projectId}")]
        [ProducesResponseType(typeof(DetailedReadProjectFinalReportOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadProjectFinalReportOutput>> GetByProjectId(Guid? projectId)
        {
            if (projectId == null)
            {
                return BadRequest("O ID do projeto informado não pode ser nulo.");
            }

            try
            {
                var model = await _getProjectFinalReportByProjectId.ExecuteAsync(projectId);
                if (model == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Relatório de Projeto encontrado para o ID do projeto {projectId}.", projectId);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Cria relatório final de projeto.
        /// </summary>
        /// <param name="request">Dados para criação de relatório final de projeto.</param>
        /// <returns>Relatório de projeto criado.</returns>
        /// <response code="201">Relatório de projeto criado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPost]
        [ProducesResponseType(typeof(DetailedReadProjectFinalReportOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadProjectFinalReportOutput>> Create([FromBody] CreateProjectFinalReportInput request)
        {
            try
            {
                var model = await _createProjectFinalReport.ExecuteAsync(request);
                _logger.LogInformation("Relatório de Projeto criado: {id}", model?.Id);
                return CreatedAtAction(nameof(GetById), new { id = model?.Id }, model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza relatório final de projeto.
        /// </summary>
        /// <param name="id">Id do relatório final de projeto.</param>
        /// <param name="request">Dados para atualização de relatório final de projeto.</param>
        /// <returns>Relatório de projeto atualizado.</returns>
        /// <response code="200">Relatório de projeto atualizado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Relatório de projeto não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DetailedReadProjectFinalReportOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadProjectFinalReportOutput>> Update(Guid? id, [FromBody] UpdateProjectFinalReportInput request)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _updateProjectFinalReport.ExecuteAsync(id.Value, request);
                if (model == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Relatório de Projeto atualizado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove relatório final de projeto.
        /// </summary>
        /// <param name="id">Id do relatório final de projeto.</param>
        /// <returns>Relatório de projeto removido.</returns>
        /// <response code="200">Relatório de projeto removido.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Relatório de projeto não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DetailedReadProjectFinalReportOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadProjectFinalReportOutput>> Delete(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _deleteProjectFinalReport.ExecuteAsync(id.Value);
                if (model == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Relatório de Projeto removido: {id}", model?.Id);
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