using Adapters.DTOs.Course;
using Adapters.Proxies.Course;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CourseController : ControllerBase
    {
        #region Global Scope
        private readonly ICourseService _noticeService;
        private readonly ILogger<CourseController> _logger;
        public CourseController(ICourseService noticeService, ILogger<CourseController> logger)
        {
            _noticeService = noticeService;
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
        public async Task<ActionResult<DetailedReadCourseDTO>> GetById(Guid? id)
        {
            if (id == null)
            {
                string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _noticeService.GetById(id);
                _logger.LogInformation($"Curso encontrado para o id {id}.");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas os editais ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todas os editais ativos</returns>
        /// <response code="200">Retorna todas os editais ativos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadCourseDTO>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _noticeService.GetAll(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum Curso encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation($"Cursos encontrados: {models.Count()}");
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
        public async Task<ActionResult<DetailedReadCourseDTO>> Create([FromForm] CreateCourseDTO dto)
        {
            try
            {
                var model = await _noticeService.Create(dto) as DetailedReadCourseDTO;
                _logger.LogInformation($"Curso criado: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
        public async Task<ActionResult<DetailedReadCourseDTO>> Update(Guid? id, [FromForm] UpdateCourseDTO dto)
        {
            try
            {
                var model = await _noticeService.Update(id, dto) as DetailedReadCourseDTO;
                _logger.LogInformation($"Curso atualizado: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
        public async Task<ActionResult<DetailedReadCourseDTO>> Delete(Guid? id)
        {
            if (id == null)
            {
                string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _noticeService.Delete(id.Value) as DetailedReadCourseDTO;
                _logger.LogInformation($"Curso removido: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}