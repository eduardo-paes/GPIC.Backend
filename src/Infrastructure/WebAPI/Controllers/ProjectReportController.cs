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
        private readonly IGetProjectFinalReportById _getById;
        private readonly IGetProjectFinalReportsByProjectId _getByProjectId;
        private readonly ICreateProjectFinalReport _create;
        private readonly IUpdateProjectFinalReport _update;
        private readonly IDeleteProjectFinalReport _delete;
        private readonly ILogger<ProjectFinalReportController> _logger;

        /// <summary>
        /// Construtor do Controller de Relatório de Projeto.
        /// </summary>
        /// <param name="getById">Use Case de busca de relatório de projeto por Id.</param>
        /// <param name="getByProjectId">Use Case de busca de relatório de projeto por Id do projeto.</param>
        /// <param name="create">Use Case de criação de relatório de projeto.</param>
        /// <param name="update">Use Case de atualização de relatório de projeto.</param>
        /// <param name="delete">Use Case de remoção de relatório de projeto.</param>
        /// <param name="logger">Logger.</param>
        public ProjectFinalReportController(IGetProjectFinalReportById getById,
            IGetProjectFinalReportsByProjectId getByProjectId,
            ICreateProjectFinalReport create,
            IUpdateProjectFinalReport update,
            IDeleteProjectFinalReport delete,
            ILogger<ProjectFinalReportController> logger)
        {
            _getById = getById;
            _getByProjectId = getByProjectId;
            _create = create;
            _update = update;
            _delete = delete;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Busca relatório de projeto pelo Id.
        /// </summary>
        /// <param name="id">Id do relatório de projeto.</param>
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
                var model = await _getById.ExecuteAsync(id);
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
        /// Busca relatórios de projeto pelo Id do projeto.
        /// </summary>
        /// <param name="projectId">Id do projeto.</param>
        /// <returns>Relatórios de projeto.</returns>
        /// <response code="200">Relatórios de projeto encontrados.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Relatórios de projeto não encontrados.</response>
        [HttpGet("project/{projectId}")]
        [ProducesResponseType(typeof(IEnumerable<DetailedReadProjectFinalReportOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<DetailedReadProjectFinalReportOutput>>> GetByProjectId(Guid? projectId)
        {
            if (projectId == null)
            {
                return BadRequest("O ID do projeto informado não pode ser nulo.");
            }

            try
            {
                var model = await _getByProjectId.ExecuteAsync(projectId);
                if (model == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Relatórios de Projeto encontrados para o ID do projeto {projectId}.", projectId);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Cria relatório de projeto.
        /// </summary>
        /// <param name="request">Dados para criação de relatório de projeto.</param>
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
                var model = await _create.ExecuteAsync(request);
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
        /// Atualiza relatório de projeto.
        /// </summary>
        /// <param name="id">Id do relatório de projeto.</param>
        /// <param name="request">Dados para atualização de relatório de projeto.</param>
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
                var model = await _update.ExecuteAsync(id.Value, request);
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
        /// Remove relatório de projeto.
        /// </summary>
        /// <param name="id">Id do relatório de projeto.</param>
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
                var model = await _delete.ExecuteAsync(id.Value);
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