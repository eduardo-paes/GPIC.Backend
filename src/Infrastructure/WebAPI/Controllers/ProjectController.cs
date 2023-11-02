using Application.Ports.Project;
using Application.Interfaces.UseCases.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Ports.ProjectActivity;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de projetos.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        #region Global Scope
        private readonly IAppealProject _appealProject;
        private readonly ICancelProject _cancelProject;
        private readonly IGetClosedProjects _getClosedProjects;
        private readonly IGetOpenProjects _getOpenProjects;
        private readonly IGetProjectById _getProjectById;
        private readonly IOpenProject _openProject;
        private readonly ISubmitProject _submitProject;
        private readonly IUpdateProject _updateProject;
        private readonly IGetActivitiesByProjectId _getActivitiesByProjectId;
        private readonly ILogger<ProjectController> _logger;

        /// <summary>
        /// Construtor do Controller de projetos.
        /// </summary>
        /// <param name="getProjectById">Serviço de obtenção de projeto pelo id.</param>
        /// <param name="getOpenProjects">Serviço de obtenção de projetos abertos.</param>
        /// <param name="getClosedProjects">Serviço de obtenção de projetos fechados.</param>
        /// <param name="openProject">Serviço de abertura de projeto.</param>
        /// <param name="updateProject">Serviço de atualização de projeto.</param>
        /// <param name="cancelProject">Serviço de cancelamento de projeto.</param>
        /// <param name="appealProject">Serviço de recurso de projeto.</param>
        /// <param name="submitProject">Serviço de submissão de projeto.</param>
        /// <param name="getActivitiesByProjectId">Serviço de obtenção de atividades de projeto.</param>
        /// <param name="logger">Serviço de log.</param>
        public ProjectController(IGetProjectById getProjectById,
            IGetOpenProjects getOpenProjects,
            IGetClosedProjects getClosedProjects,
            IOpenProject openProject,
            IUpdateProject updateProject,
            ICancelProject cancelProject,
            IAppealProject appealProject,
            ISubmitProject submitProject,
            IGetActivitiesByProjectId getActivitiesByProjectId,
            ILogger<ProjectController> logger)
        {
            _getProjectById = getProjectById;
            _getOpenProjects = getOpenProjects;
            _getClosedProjects = getClosedProjects;
            _openProject = openProject;
            _updateProject = updateProject;
            _cancelProject = cancelProject;
            _appealProject = appealProject;
            _submitProject = submitProject;
            _getActivitiesByProjectId = getActivitiesByProjectId;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Busca projeto pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Projeto correspondente</returns>
        /// <response code="200">Retorna projeto correspondente</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhum projeto encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProjectOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadProjectOutput>> GetProjectById(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do projeto não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var project = await _getProjectById.ExecuteAsync(id.Value);
                if (project == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Projeto encontrado para o ID {id}.", id);
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Busca atividades de projeto pelo id do projeto.
        /// </summary>
        /// <param name="projectId">Id do projeto</param>
        /// <returns>Atividades de projeto correspondentes</returns>
        /// <response code="200">Retorna atividades de projeto correspondentes</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhuma atividade encontrada.</response>
        [HttpGet("activity/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DetailedReadProjectActivityOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<DetailedReadProjectActivityOutput>>> GetActivitiesByProjectId(Guid? projectId)
        {
            var activities = await _getActivitiesByProjectId.ExecuteAsync(projectId);
            if (activities == null || !activities.Any())
            {
                const string errorMessage = "Nenhuma atividade encontrada.";
                _logger.LogWarning(errorMessage);
                return NotFound(errorMessage);
            }
            _logger.LogInformation("Atividades encontradas: {quantidade}", activities.Count());
            return Ok(activities);
        }


        /// <summary>
        /// Busca projetos abertos.
        /// </summary>
        /// <param name="skip">Quantidade de registros a serem ignorados.</param>
        /// <param name="take">Quantidade de registros a serem retornados.</param>
        /// <param name="onlyMyProjects">Indica que apenas os projetos relacionados ao usuário serão retornados.</param>
        /// <returns>Projetos abertos do usuário logado.</returns>
        /// <response code="200">Retorna projetos abertos do usuário logado.</response>
        /// <response code="400">Ocorreu um erro ao buscar projetos abertos.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhum projeto encontrado.</response>
        [HttpGet("opened")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResumedReadProjectOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<ResumedReadProjectOutput>>> GetOpenProjects(int skip = 0, int take = 50, bool onlyMyProjects = true)
        {
            var projects = await _getOpenProjects.ExecuteAsync(skip, take, onlyMyProjects);
            if (projects == null || !projects.Any())
            {
                const string errorMessage = "Nenhum projeto aberto encontrado.";
                _logger.LogWarning(errorMessage);
                return NotFound(errorMessage);
            }
            _logger.LogInformation("Projetos abertos encontrados: {quantidade}", projects.Count);
            return Ok(projects);
        }

        /// <summary>
        /// Busca projetos fechados.
        /// </summary>
        /// <param name="skip">Quantidade de registros a serem ignorados.</param>
        /// <param name="take">Quantidade de registros a serem retornados.</param>
        /// <param name="onlyMyProjects">Indica que apenas os projetos relacionados ao usuário serão retornados.</param>
        /// <returns>Projetos fechados do usuário logado.</returns>
        /// <response code="200">Retorna projetos fechados do usuário logado.</response>
        /// <response code="400">Ocorreu um erro ao buscar projetos fechados.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhum projeto encontrado.</response>
        [HttpGet("closed")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResumedReadProjectOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<ResumedReadProjectOutput>>> GetClosedProjects(int skip = 0, int take = 50, bool onlyMyProjects = true)
        {
            var projects = await _getClosedProjects.ExecuteAsync(skip, take, onlyMyProjects);
            if (projects == null || !projects.Any())
            {
                const string errorMessage = "Nenhum projeto fechado encontrado.";
                _logger.LogWarning(errorMessage);
                return NotFound(errorMessage);
            }
            _logger.LogInformation("Projetos fechados encontrados: {quantidade}", projects.Count);
            return Ok(projects);
        }

        /// <summary>
        /// Cria projeto.
        /// </summary>
        /// <param name="request">Informações de abertura do projeto</param>
        /// <returns>Projeto criado</returns>
        /// <response code="200">Retorna projeto criado</response>
        /// <response code="400">Ocorreu um erro ao criar projeto.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResumedReadProjectOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<ResumedReadProjectOutput>> OpenProject([FromBody] OpenProjectInput request)
        {
            try
            {
                var project = await _openProject.ExecuteAsync(request);
                _logger.LogInformation("Projeto aberto: {id}", project?.Id);
                return CreatedAtAction(nameof(GetProjectById), new { id = project?.Id }, project);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza projeto.
        /// </summary>
        /// <param name="projectId">Id do projeto</param>
        /// <param name="request">Informações de atualização do projeto</param>
        /// <returns>Projeto atualizado</returns>
        /// <response code="200">Retorna projeto atualizado</response>
        /// <response code="400">Ocorreu um erro ao atualizar projeto.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhum projeto encontrado.</response>
        [HttpPut("{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResumedReadProjectOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<ResumedReadProjectOutput>> UpdateProject(Guid? projectId, [FromBody] UpdateProjectInput request)
        {
            if (projectId == null)
            {
                const string errorMessage = "O ID do projeto não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var project = await _updateProject.ExecuteAsync(projectId.Value, request);
                if (project == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Projeto atualizado: {id}", project?.Id);
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cancela projeto.
        /// </summary>
        /// <param name="projectId">Id do projeto</param>
        /// <param name="observation">Observação do cancelamento</param>
        /// <returns>Projeto cancelado</returns>
        /// <response code="200">Retorna projeto cancelado</response>
        /// <response code="400">Ocorreu um erro ao cancelar projeto.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Projeto não encontrado.</response>
        [HttpDelete("{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResumedReadProjectOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<ResumedReadProjectOutput>> CancelProject(Guid? projectId, string? observation)
        {
            if (projectId == null)
            {
                const string errorMessage = "O ID do projeto não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var project = await _cancelProject.ExecuteAsync(projectId.Value, observation);
                if (project == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Projeto cancelado: {id}", project?.Id);
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Solicita recurso para o projeto.
        /// </summary>
        /// <param name="projectId">Id do projeto</param>
        /// <param name="appealDescription">Descrição do recurso</param>
        /// <returns>Projeto com recurso solicitado</returns>
        /// <response code="200">Retorna projeto com recurso solicitado</response>
        /// <response code="400">Ocorreu um erro ao solicitar recurso para o projeto.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPut("appeal/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResumedReadProjectOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<ResumedReadProjectOutput>> AppealProject(Guid? projectId, string? appealDescription)
        {
            if (projectId == null)
            {
                const string errorMessage = "O ID do projeto não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var project = await _appealProject.ExecuteAsync(projectId.Value, appealDescription);
                _logger.LogInformation("Recurso do projeto solicitado: {id}", project?.Id);
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Submete projeto.
        /// </summary>
        /// <param name="projectId">Id do projeto</param>
        /// <returns>Projeto submetido</returns>
        /// <response code="200">Retorna projeto submetido</response>
        /// <response code="400">Ocorreu um erro ao submeter projeto.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPut("submit/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResumedReadProjectOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<ResumedReadProjectOutput>> SubmitProject(Guid? projectId)
        {
            if (projectId == null)
            {
                const string errorMessage = "O ID do projeto não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var project = await _submitProject.ExecuteAsync(projectId.Value);
                _logger.LogInformation("Projeto submetido: {id}", project?.Id);
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}