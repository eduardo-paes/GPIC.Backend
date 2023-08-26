using Application.Ports.Course;
using Application.Interfaces.UseCases.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
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
        private readonly IGetCourseById _getById;
        private readonly IGetCourses _getAll;
        private readonly ICreateCourse _create;
        private readonly IUpdateCourse _update;
        private readonly IDeleteCourse _delete;
        private readonly ILogger<CourseController> _logger;
        /// <summary>
        /// Construtor do Controller de Curso.
        /// </summary>
        /// <param name="getById">Serviço de obtenção de curso pelo id.</param>
        /// <param name="getAll">Serviço de obtenção de todos os cursos ativos.</param>
        /// <param name="create">Serviço de criação de curso.</param>
        /// <param name="update">Serviço de atualização de curso.</param>
        /// <param name="delete">Serviço de remoção de curso.</param>
        /// <param name="logger">Serviço de log.</param>
        public CourseController(IGetCourseById getById,
            IGetCourses getAll,
            ICreateCourse create,
            IUpdateCourse update,
            IDeleteCourse delete,
            ILogger<CourseController> logger)
        {
            _getById = getById;
            _getAll = getAll;
            _create = create;
            _update = update;
            _delete = delete;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Busca curso pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Curso correspondente</returns>
        /// <response code="200">Retorna curso correspondente</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="404">Curso não encontrado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadCourseOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadCourseOutput>> GetById(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do curso não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var course = await _getById.ExecuteAsync(id);
                if (course == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Curso encontrado para o ID {id}.", id);
                return Ok(course);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas os cursos ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todas os cursos ativos</returns>
        /// <response code="200">Retorna todas os cursos ativos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResumedReadCourseOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<ResumedReadCourseOutput>>> GetAll(int skip = 0, int take = 50)
        {
            try
            {
                var courses = await _getAll.ExecuteAsync(skip, take);
                if (courses == null || !courses.Any())
                {
                    const string errorMessage = "Nenhum curso encontrado.";
                    _logger.LogWarning(errorMessage);
                    return NotFound(errorMessage);
                }
                _logger.LogInformation("Cursos encontrados: {quantidade}", courses.Count());
                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cria curso.
        /// </summary>
        /// <param></param>
        /// <returns>Curso criado</returns>
        /// <response code="201">Retorna curso criado</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailedReadCourseOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadCourseOutput>> Create([FromBody] CreateCourseInput request)
        {
            try
            {
                var createdCourse = await _create.ExecuteAsync(request);
                _logger.LogInformation("Curso criado: {id}", createdCourse?.Id);
                return CreatedAtAction(nameof(GetById), new { id = createdCourse?.Id }, createdCourse);
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
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadCourseOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadCourseOutput>> Update(Guid? id, [FromBody] UpdateCourseInput request)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do curso não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var updatedCourse = await _update.ExecuteAsync(id.Value, request);
                if (updatedCourse == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Curso atualizado: {id}", updatedCourse?.Id);
                return Ok(updatedCourse);
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
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadCourseOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadCourseOutput>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do curso não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var deletedCourse = await _delete.ExecuteAsync(id.Value);
                if (deletedCourse == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Curso removido: {id}", deletedCourse?.Id);
                return Ok(deletedCourse);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}