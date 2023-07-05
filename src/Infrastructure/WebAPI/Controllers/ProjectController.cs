using Adapters.Gateways.Project;
using Adapters.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    /// <summary>
    /// Controller de projetos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        #region Global Scope
        private readonly IProjectPresenterController _service;
        private readonly ILogger<ProjectController> _logger;

        /// <summary>
        /// Construtor do Controller de projetos.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public ProjectController(
            IProjectPresenterController service,
            ILogger<ProjectController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca projeto pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Projeto correspondente</returns>
        /// <response code="200">Retorna projeto correspondente</response>
        /// <response code="404">Nenhum projeto encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadProjectResponse>> GetProjectById(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.GetProjectById(id);
                _logger.LogInformation("Projeto encontrado para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca projetos abertos.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="onlyMyProjects"></param>
        /// <returns>Projetos abertos do usuário logado.</returns>
        /// <response code="200">Retorna projetos abertos do usuário logado.</response>
        /// <response code="404">Nenhum projeto encontrado.</response>
        [HttpGet("opened")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadProjectResponse>>> GetOpenProjects(int skip = 0, int take = 50, bool onlyMyProjects = true)
        {
            var models = await _service.GetOpenProjects(skip, take, onlyMyProjects);
            if (models == null)
            {
                const string msg = "Nenhum Projeto encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Projetos encontrados: {quantidade}", models.Count);
            return Ok(models);
        }

        /// <summary>
        /// Busca projetos fechados.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="onlyMyProjects"></param>
        /// <returns>Projetos fechados do usuário logado.</returns>
        /// <response code="200">Retorna projetos fechados do usuário logado.</response>
        /// <response code="404">Nenhum projeto encontrado.</response>
        [HttpGet("closed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadProjectResponse>>> GetClosedProjects(int skip = 0, int take = 50, bool onlyMyProjects = true)
        {
            var models = await _service.GetClosedProjects(skip, take, onlyMyProjects);
            if (models == null)
            {
                const string msg = "Nenhum Projeto encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Projetos encontrados: {quantidade}", models.Count);
            return Ok(models);
        }

        /// <summary>
        /// Cria projeto.
        /// </summary>
        /// <param name="request">Informações de abertura do projeto</param>
        /// <returns>Projeto criado</returns>
        /// <response code="200">Retorna projeto criado</response>
        /// <response code="400">Ocorreu um erro ao criar projeto.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<ResumedReadProjectResponse>> OpenProject([FromBody] OpenProjectRequest request)
        {
            try
            {
                var model = await _service.OpenProject(request);
                _logger.LogInformation("Projeto aberto: {id}", model?.Id);
                return Ok(model);
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
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Projeto atualizado</returns>
        /// <response code="200">Retorna projeto atualizado</response>
        /// <response code="400">Ocorreu um erro ao atualizar projeto.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResumedReadProjectResponse>> UpdateProject(Guid? id, [FromBody] UpdateProjectRequest request)
        {
            try
            {
                var model = await _service.UpdateProject(id, request);
                _logger.LogInformation("Projeto atualizado: {id}", model?.Id);
                return Ok(model);
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
        /// <param name="id"></param>
        /// <param name="observation"></param>
        /// <returns>Projeto cancelado</returns>
        /// <response code="200">Retorna projeto cancelado</response>
        /// <response code="400">Ocorreu um erro ao cancelar projeto.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResumedReadProjectResponse>> CancelProject(Guid? id, string? observation)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.CancelProject(id.Value, observation);
                _logger.LogInformation("Projeto removido: {id}", model?.Id);
                return Ok(model);
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
        /// <param name="id"></param>
        /// <param name="appealDescription"></param>
        /// <returns>Projeto com recurso solicitado</returns>
        /// <response code="200">Retorna projeto com recurso solicitado</response>
        /// <response code="400">Ocorreu um erro ao solicitar recurso para o projeto.</response>
        [HttpPut("appeal/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResumedReadProjectResponse>> AppealProject(Guid? id, string? appealDescription)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.AppealProject(id.Value, appealDescription);
                _logger.LogInformation("Recurso do Projeto: {id}", model?.Id);
                return Ok(model);
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
        /// <param name="id"></param>
        /// <returns>Projeto submetido</returns>
        /// <response code="200">Retorna projeto submetido</response>
        /// <response code="400">Ocorreu um erro ao submeter projeto.</response>
        [HttpPut("submit/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResumedReadProjectResponse>> SubmitProject(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.SubmitProject(id.Value);
                _logger.LogInformation("Submissão do Projeto: {id}", model?.Id);
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