using Adapters.Gateways.Project;
using Adapters.Gateways.ProjectEvaluation;
using Adapters.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
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
        private readonly IProjectEvaluationPresenterController _service;
        private readonly ILogger<ProjectEvaluationController> _logger;

        /// <summary>
        /// Construtor do Controller de projetos.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public ProjectEvaluationController(
            IProjectEvaluationPresenterController service,
            ILogger<ProjectEvaluationController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca avaliação do projeto pelo id do projeto.
        /// </summary>
        /// <param></param>
        /// <returns>Avaliação do projeto correspondente</returns>
        /// <response code="200">Retorna avaliação do projeto correspondente</response>
        /// <response code="404">Nenhum avaliação do projeto encontrado.</response>
        [HttpGet("{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadProjectEvaluationResponse>> GetEvaluationByProjectId(Guid? projectId)
        {
            if (projectId == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.GetEvaluationByProjectId(projectId);
                _logger.LogInformation("Avaliação do projeto encontrado para o id {id}.", projectId);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Realiza a avaliação da submissão de um projeto.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Projeto correspondente</returns>
        /// <response code="200">Retorna avaliação do projeto correspondente</response>
        [HttpPost("submission")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<DetailedReadProjectResponse>> EvaluateSubmissionProject([FromBody] EvaluateSubmissionProjectRequest request)
        {
            try
            {
                var model = await _service.EvaluateSubmissionProject(request);
                _logger.LogInformation("Avaliação da submissão do projeto {id} realizada.", model?.Id);
                return Ok(model);
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
        [HttpPut("appeal")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadProjectResponse>> EvaluateAppealProjectRequest([FromBody] EvaluateAppealProjectRequest request)
        {
            try
            {
                var model = await _service.EvaluateAppealProject(request);
                _logger.LogInformation("Avaliação do recurso do projeto {id} realizada.", model?.Id);
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