using Adapters.DTOs.Student;
using Adapters.Proxies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    /// <summary>
    /// Controller de Estudante.
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class StudentController : ControllerBase
    {
        #region Global Scope
        private readonly IStudentService _service;
        private readonly ILogger<StudentController> _logger;
        /// <summary>
        /// Construtor do Controller de Estudante.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public StudentController(IStudentService service, ILogger<StudentController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca Estudante pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante correspondente</returns>
        /// <response code="200">Retorna Estudante correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadStudentDTO>> GetById(Guid? id)
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
                _logger.LogInformation("Estudante encontrado para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas os Estudante ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todas os Estudante ativos</returns>
        /// <response code="200">Retorna todas os Estudante ativos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadStudentDTO>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _service.GetAll(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum Estudante encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Estudante encontrados: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Cria Estudante.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante criado</returns>
        /// <response code="200">Retorna Estudante criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<DetailedReadStudentDTO>> Create([FromBody] CreateStudentDTO dto)
        {
            try
            {
                var model = await _service.Create(dto) as DetailedReadStudentDTO;
                _logger.LogInformation("Estudante criado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza Estudante.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante atualizado</returns>
        /// <response code="200">Retorna Estudante atualizado</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN, STUDENT")]
        public async Task<ActionResult<DetailedReadStudentDTO>> Update(Guid? id, [FromBody] UpdateStudentDTO dto)
        {
            try
            {
                var model = await _service.Update(id, dto) as DetailedReadStudentDTO;
                _logger.LogInformation("Estudante atualizado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove Estudante.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante removido</returns>
        /// <response code="200">Retorna Estudante removido</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN, STUDENT")]
        public async Task<ActionResult<DetailedReadStudentDTO>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value) as DetailedReadStudentDTO;
                _logger.LogInformation("Estudante removido: {id}", model?.Id);
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