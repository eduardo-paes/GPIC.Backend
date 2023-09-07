using Application.Ports.ProjectEvaluation;
using Application.Interfaces.UseCases.ProjectEvaluation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Ports.Project;
using Application.Interfaces.UseCases.Project;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de projetos.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProjectEvaluationController : ControllerBase
    {
        #region Global Scope
        private readonly IEvaluateAppealProject _evaluateAppealProject;
        private readonly IEvaluateSubmissionProject _evaluateSubmissionProject;
        private readonly IEvaluateStudentDocuments _evaluateStudentDocuments;
        private readonly IGetEvaluationByProjectId _getEvaluationByProjectId;
        private readonly IGetProjectsToEvaluate _getProjectsToEvaluate;
        private readonly ILogger<ProjectEvaluationController> _logger;

        /// <summary>
        /// Construtor do Controller de projetos.
        /// </summary>
        /// <param name="evaluateAppealProject">Serviço de avaliação de recurso de projeto.</param>
        /// <param name="evaluateSubmissionProject">Serviço de avaliação de submissão de projeto.</param>
        /// <param name="evaluateStudentDocuments">Serviço de avaliação de documentos do estudante.</param>
        /// <param name="getEvaluationByProjectId">Serviço de obtenção de avaliação de projeto pelo id do projeto.</param>
        /// <param name="getProjectsToEvaluate">Serviço de obtenção de projetos para avaliação.</param>
        /// <param name="logger">Serviço de log.</param>
        public ProjectEvaluationController(IEvaluateAppealProject evaluateAppealProject,
            IEvaluateSubmissionProject evaluateSubmissionProject,
            IEvaluateStudentDocuments evaluateStudentDocuments,
            IGetEvaluationByProjectId getEvaluationByProjectId,
            IGetProjectsToEvaluate getProjectsToEvaluate,
            ILogger<ProjectEvaluationController> logger)
        {
            _evaluateAppealProject = evaluateAppealProject;
            _evaluateSubmissionProject = evaluateSubmissionProject;
            _evaluateStudentDocuments = evaluateStudentDocuments;
            _getEvaluationByProjectId = getEvaluationByProjectId;
            _getProjectsToEvaluate = getProjectsToEvaluate;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Busca projetos em aberto e que se encontram no estágio de avaliação (Submetido, Recurso, Análise de Documentos).
        /// </summary>
        /// <param name="skip">Quantidade de registros a serem ignorados.</param>
        /// <param name="take">Quantidade de registros a serem retornados.</param>
        /// <returns>Projetos para avaliação.</returns>
        /// <response code="200">Retorna projetos para avaliação.</response>
        /// <response code="400">Ocorreu um erro ao buscar projetos para avaliação.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProjectOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<IEnumerable<DetailedReadProjectOutput>>> GetProjectsToEvaluate(int skip = 0, int take = 50)
        {
            try
            {
                var projects = await _getProjectsToEvaluate.ExecuteAsync(skip, take);
                if (projects == null || !projects.Any())
                {
                    const string errorMessage = "Nenhum projeto para avaliação encontrado.";
                    _logger.LogWarning(errorMessage);
                    return NotFound(errorMessage);
                }
                _logger.LogInformation("Projetos para avaliação encontrados: {quantidade}", projects.Count);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar projetos para avaliação: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Busca avaliação do projeto pelo id do projeto.
        /// </summary>
        /// <param name="projectId">Id do projeto.</param>
        /// <returns>Avaliação do projeto correspondente</returns>
        /// <response code="200">Retorna avaliação do projeto correspondente</response>
        /// <response code="400">Retorna mensagem de erro</response>
        /// <response code="401">Retorna mensagem de erro</response>
        /// <response code="404">Nenhum avaliação do projeto encontrado.</response>
        [HttpGet("{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProjectEvaluationOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadProjectEvaluationOutput>> GetEvaluationByProjectId(Guid? projectId)
        {
            if (projectId == null)
            {
                const string errorMessage = "O ID do projeto não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var evaluation = await _getEvaluationByProjectId.ExecuteAsync(projectId.Value);
                if (evaluation == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Avaliação do projeto encontrada para o ID {id}.", projectId);
                return Ok(evaluation);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza a avaliação da submissão de um projeto.
        /// </summary>
        /// <param name="request">Dados da avaliação.</param>
        /// <returns>Projeto correspondente</returns>
        /// <response code="200">Retorna avaliação do projeto correspondente</response>
        /// <response code="400">Retorna mensagem de erro</response>
        /// <response code="401">Retorna mensagem de erro</response>
        [HttpPost("submission")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProjectOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<DetailedReadProjectOutput>> EvaluateSubmissionProject([FromBody] EvaluateSubmissionProjectInput request)
        {
            try
            {
                var evaluatedProject = await _evaluateSubmissionProject.ExecuteAsync(request);
                _logger.LogInformation("Avaliação da submissão do projeto {id} realizada.", evaluatedProject?.Id);
                return Ok(evaluatedProject);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza a avaliação do recurso de um projeto.
        /// </summary>
        /// <param name="request">Dados da avaliação.</param>
        /// <returns>Projeto correspondente</returns>
        /// <response code="200">Retorna avaliação do projeto correspondente</response>
        /// <response code="400">Retorna mensagem de erro</response>
        /// <response code="401">Retorna mensagem de erro</response>
        [HttpPut("appeal")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProjectOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<DetailedReadProjectOutput>> EvaluateAppealProjectRequest([FromBody] EvaluateAppealProjectInput request)
        {
            try
            {
                var evaluatedProject = await _evaluateAppealProject.ExecuteAsync(request);
                _logger.LogInformation("Avaliação do recurso do projeto {id} realizada.", evaluatedProject?.Id);
                return Ok(evaluatedProject);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza a avaliação dos documentos de um estudante.
        /// </summary>
        /// <param name="request">Dados da avaliação.</param>
        /// <returns>Projeto correspondente</returns>
        /// <response code="200">Retorna avaliação do projeto correspondente</response>
        /// <response code="400">Retorna mensagem de erro</response>
        /// <response code="401">Retorna mensagem de erro</response>
        [HttpPut("documents")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProjectOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<DetailedReadProjectOutput>> EvaluateStudentDocuments([FromBody] EvaluateStudentDocumentsInput request)
        {
            try
            {
                var evaluatedProject = await _evaluateStudentDocuments.ExecuteAsync(request);
                _logger.LogInformation("Avaliação dos documentos do estudante do projeto {id} realizada.", evaluatedProject?.Id);
                return Ok(evaluatedProject);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}