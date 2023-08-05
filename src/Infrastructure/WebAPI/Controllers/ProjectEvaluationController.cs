using Application.Ports.ProjectEvaluation;
using Application.Interfaces.UseCases.ProjectEvaluation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Ports.Project;

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
        private readonly IGetEvaluationByProjectId _getEvaluationByProjectId;
        private readonly ILogger<ProjectEvaluationController> _logger;

        /// <summary>
        /// Construtor do Controller de projetos.
        /// </summary>
        /// <param name="evaluateAppealProject">Serviço de avaliação de recurso de projeto.</param>
        /// <param name="evaluateSubmissionProject">Serviço de avaliação de submissão de projeto.</param>
        /// <param name="getEvaluationByProjectId">Serviço de obtenção de avaliação de projeto pelo id do projeto.</param>
        /// <param name="logger">Serviço de log.</param>
        public ProjectEvaluationController(IEvaluateAppealProject evaluateAppealProject,
            IEvaluateSubmissionProject evaluateSubmissionProject,
            IGetEvaluationByProjectId getEvaluationByProjectId,
            ILogger<ProjectEvaluationController> logger)
        {
            _evaluateAppealProject = evaluateAppealProject;
            _evaluateSubmissionProject = evaluateSubmissionProject;
            _getEvaluationByProjectId = getEvaluationByProjectId;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Busca avaliação do projeto pelo id do projeto.
        /// </summary>
        /// <param></param>
        /// <returns>Avaliação do projeto correspondente</returns>
        /// <response code="200">Retorna avaliação do projeto correspondente</response>
        /// <response code="400">Retorna mensagem de erro</response>
        /// <response code="401">Retorna mensagem de erro</response>
        /// <response code="404">Nenhum avaliação do projeto encontrado.</response>
        [HttpGet("{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProjectEvaluationOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
                    return NotFound();
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
        /// <param name="request"></param>
        /// <returns>Projeto correspondente</returns>
        /// <response code="200">Retorna avaliação do projeto correspondente</response>
        /// <response code="400">Retorna mensagem de erro</response>
        /// <response code="401">Retorna mensagem de erro</response>
        [HttpPost("submission")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProjectOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <param name="request"></param>
        /// <returns>Projeto correspondente</returns>
        /// <response code="200">Retorna avaliação do projeto correspondente</response>
        /// <response code="400">Retorna mensagem de erro</response>
        /// <response code="401">Retorna mensagem de erro</response>
        [HttpPut("appeal")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProjectOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
    }
}