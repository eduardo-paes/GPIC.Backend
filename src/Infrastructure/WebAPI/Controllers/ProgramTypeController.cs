using Adapters.DTOs.ProgramType;
using Adapters.Proxies.ProgramType;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ProgramTypeController : ControllerBase
    {
        #region Global Scope
        private readonly IProgramTypeService _programTypeService;
        private readonly ILogger<ProgramTypeController> _logger;
        public ProgramTypeController(IProgramTypeService noticeService, ILogger<ProgramTypeController> logger)
        {
            _programTypeService = noticeService;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca tipo de programa pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Tipo de Programa correspondente</returns>
        /// <response code="200">Retorna tipo de programa correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadProgramTypeDTO>> GetById(Guid? id)
        {
            if (id == null)
            {
                string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _programTypeService.GetById(id);
                _logger.LogInformation($"Tipo de Programa encontrado para o id {id}.");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas os tipos de programas ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todas os tipos de programas ativos</returns>
        /// <response code="200">Retorna todas os tipos de programas ativos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadProgramTypeDTO>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _programTypeService.GetAll(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum Tipo de Programa encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation($"Tipos de Programas encontrados: {models.Count()}");
            return Ok(models);
        }

        /// <summary>
        /// Cria tipo de programa.
        /// </summary>
        /// <param></param>
        /// <returns>Tipo de Programa criado</returns>
        /// <response code="200">Retorna tipo de programa criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadProgramTypeDTO>> Create([FromForm] CreateProgramTypeDTO dto)
        {
            try
            {
                var model = await _programTypeService.Create(dto) as DetailedReadProgramTypeDTO;
                _logger.LogInformation($"Tipo de Programa criado: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza tipo de programa.
        /// </summary>
        /// <param></param>
        /// <returns>Tipo de Programa atualizado</returns>
        /// <response code="200">Retorna tipo de programa atualizado</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<DetailedReadProgramTypeDTO>> Update(Guid? id, [FromForm] UpdateProgramTypeDTO dto)
        {
            try
            {
                var model = await _programTypeService.Update(id, dto) as DetailedReadProgramTypeDTO;
                _logger.LogInformation($"Tipo de Programa atualizado: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove tipo de programa.
        /// </summary>
        /// <param></param>
        /// <returns>Tipo de Programa removido</returns>
        /// <response code="200">Retorna tipo de programa removido</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetailedReadProgramTypeDTO>> Delete(Guid? id)
        {
            if (id == null)
            {
                string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _programTypeService.Delete(id.Value) as DetailedReadProgramTypeDTO;
                _logger.LogInformation($"Tipo de Programa removido: {model.Id}");
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