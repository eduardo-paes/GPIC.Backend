using Application.Ports.ProjectPartialReport;
using Application.Interfaces.UseCases.ProjectPartialReport;
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
    public class ProjectPartialReportController : ControllerBase
    {
        #region Global Scope
        private readonly IGetProjectPartialReportById _getProjectPartialReportById;
        private readonly IGetProjectPartialReportByProjectId _getProjectPartialReportByProjectId;
        private readonly ICreateProjectPartialReport _createProjectPartialReport;
        private readonly IUpdateProjectPartialReport _updateProjectPartialReport;
        private readonly IDeleteProjectPartialReport _deleteProjectPartialReport;
        private readonly ILogger<ProjectPartialReportController> _logger;

        /// <summary>
        /// Construtor do Controller de Relatório de Projeto.
        /// </summary>
        /// <param name="getProjectPartialReportById">Use Case de busca de relatório parcial de projeto por Id.</param>
        /// <param name="getProjectPartialReportByProjectId">Use Case de busca de relatório parcial de projeto por Id do projeto.</param>
        /// <param name="createProjectPartialReport">Use Case de criação de relatório parcial de projeto.</param>
        /// <param name="updateProjectPartialReport">Use Case de atualização de relatório parcial de projeto.</param>
        /// <param name="deleteProjectPartialReport">Use Case de remoção de relatório parcial de projeto.</param>
        /// <param name="logger">Logger.</param>
        public ProjectPartialReportController(IGetProjectPartialReportById getProjectPartialReportById,
            IGetProjectPartialReportByProjectId getProjectPartialReportByProjectId,
            ICreateProjectPartialReport createProjectPartialReport,
            IUpdateProjectPartialReport updateProjectPartialReport,
            IDeleteProjectPartialReport deleteProjectPartialReport,
            ILogger<ProjectPartialReportController> logger)
        {
            _getProjectPartialReportById = getProjectPartialReportById;
            _getProjectPartialReportByProjectId = getProjectPartialReportByProjectId;
            _createProjectPartialReport = createProjectPartialReport;
            _updateProjectPartialReport = updateProjectPartialReport;
            _deleteProjectPartialReport = deleteProjectPartialReport;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Busca relatório parcial de projeto pelo Id.
        /// </summary>
        /// <param name="id">Id do relatório parcial de projeto.</param>
        /// <returns>Relatório de projeto.</returns>
        /// <response code="200">Relatório de projeto encontrado.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Relatório de projeto não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DetailedReadProjectPartialReportOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadProjectPartialReportOutput>> GetById(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _getProjectPartialReportById.ExecuteAsync(id);
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
        /// Busca relatório parcial de projeto pelo Id do projeto.
        /// </summary>
        /// <param name="projectId">Id do projeto.</param>
        /// <returns>Relatório parcial do projeto.</returns>
        /// <response code="200">Relatórios de projeto encontrados.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Relatórios de projeto não encontrados.</response>
        [HttpGet("project/{projectId}")]
        [ProducesResponseType(typeof(DetailedReadProjectPartialReportOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadProjectPartialReportOutput>> GetByProjectId(Guid? projectId)
        {
            if (projectId == null)
            {
                return BadRequest("O ID do projeto informado não pode ser nulo.");
            }

            try
            {
                var model = await _getProjectPartialReportByProjectId.ExecuteAsync(projectId);
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
        /// Cria relatório parcial de projeto.
        /// </summary>
        /// <param name="request">Dados para criação de relatório parcial de projeto.</param>
        /// <returns>Relatório de projeto criado.</returns>
        /// <response code="201">Relatório de projeto criado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPost]
        [ProducesResponseType(typeof(DetailedReadProjectPartialReportOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadProjectPartialReportOutput>> Create([FromBody] CreateProjectPartialReportInput request)
        {
            try
            {
                var model = await _createProjectPartialReport.ExecuteAsync(request);
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
        /// Atualiza relatório parcial de projeto.
        /// </summary>
        /// <param name="id">Id do relatório parcial de projeto.</param>
        /// <param name="request">Dados para atualização de relatório parcial de projeto.</param>
        /// <returns>Relatório de projeto atualizado.</returns>
        /// <response code="200">Relatório de projeto atualizado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Relatório de projeto não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DetailedReadProjectPartialReportOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadProjectPartialReportOutput>> Update(Guid? id, [FromBody] UpdateProjectPartialReportInput request)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _updateProjectPartialReport.ExecuteAsync(id.Value, request);
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
        /// Remove relatório parcial de projeto.
        /// </summary>
        /// <param name="id">Id do relatório parcial de projeto.</param>
        /// <returns>Relatório de projeto removido.</returns>
        /// <response code="200">Relatório de projeto removido.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Relatório de projeto não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DetailedReadProjectPartialReportOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadProjectPartialReportOutput>> Delete(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _deleteProjectPartialReport.ExecuteAsync(id.Value);
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