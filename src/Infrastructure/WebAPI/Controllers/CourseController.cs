using Adapters.Gateways.Course;
using Adapters.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    /// <summary>
    /// Controller de Curso.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CourseController : ControllerBase
    {
        #region Global Scope
        private readonly ICoursePresenterController _service;
        private readonly ILogger<CourseController> _logger;
        /// <summary>
        /// Construtor do Controller de Curso.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public CourseController(ICoursePresenterController service, ILogger<CourseController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca curso pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Curso correspondente</returns>
        /// <response code="200">Retorna curso correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadCourseResponse>> GetById(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.GetById(id);
                _logger.LogInformation("Curso encontrado para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas os cursos ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todas os cursos ativos</returns>
        /// <response code="200">Retorna todas os cursos ativos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadCourseResponse>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _service.GetAll(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum Curso encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Cursos encontrados: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Cria curso.
        /// </summary>
        /// <param></param>
        /// <returns>Curso criado</returns>
        /// <response code="200">Retorna curso criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadCourseResponse>> Create([FromBody] CreateCourseRequest request)
        {
            try
            {
                var model = await _service.Create(request) as DetailedReadCourseResponse;
                _logger.LogInformation("Curso criado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza curso.
        /// </summary>
        /// <param></param>
        /// <returns>Curso atualizado</returns>
        /// <response code="200">Retorna curso atualizado</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadCourseResponse>> Update(Guid? id, [FromBody] UpdateCourseRequest request)
        {
            try
            {
                var model = await _service.Update(id, request) as DetailedReadCourseResponse;
                _logger.LogInformation("Curso atualizado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove curso.
        /// </summary>
        /// <param></param>
        /// <returns>Curso removido</returns>
        /// <response code="200">Retorna curso removido</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadCourseResponse>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value) as DetailedReadCourseResponse;
                _logger.LogInformation("Curso removido: {id}", model?.Id);
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