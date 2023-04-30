using Adapters.DTOs.SubArea;
using Adapters.Proxies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    /// <summary>
    /// Controller de Sub Área.
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class SubAreaController : ControllerBase
    {
        #region Global Scope
        private readonly ISubAreaService _service;
        private readonly ILogger<SubAreaController> _logger;
        /// <summary>
        /// Construtor do Controller de Sub Área.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public SubAreaController(ISubAreaService service, ILogger<SubAreaController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca sub área pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Sub Área correspondente</returns>
        /// <response code="200">Retorna sub área correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadSubAreaDTO>> GetById(Guid? id)
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
                _logger.LogInformation("Sub Área encontrada para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas as sub áreas ativas pela área.
        /// </summary>
        /// <param></param>
        /// <returns>Todas as sub áreas ativas da área</returns>
        /// <response code="200">Retorna todas as sub áreas ativas da área</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadSubAreaDTO>>> GetSubAreasByArea(Guid? areaId, int skip = 0, int take = 50)
        {
            if (areaId == null)
            {
                const string msg = "O AreadId informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            var models = await _service.GetSubAreasByArea(areaId, skip, take);
            if (models == null)
            {
                const string msg = "Nenhuma Sub Área encontrada.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Sub Áreas encontradas: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Cria sub área.
        /// </summary>
        /// <param></param>
        /// <returns>Sub Área criada</returns>
        /// <response code="200">Retorna sub área criada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedReadSubAreaDTO>> Create([FromBody] CreateSubAreaDTO dto)
        {
            try
            {
                var model = await _service.Create(dto) as DetailedReadSubAreaDTO;
                _logger.LogInformation("Sub Área criada: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza sub área.
        /// </summary>
        /// <param></param>
        /// <returns>Sub Área atualizada</returns>
        /// <response code="200">Retorna sub área atualizada</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<DetailedReadSubAreaDTO>> Update(Guid? id, [FromBody] UpdateSubAreaDTO dto)
        {
            try
            {
                var model = await _service.Update(id, dto) as DetailedReadSubAreaDTO;
                _logger.LogInformation("Sub Área atualizada: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove sub área.
        /// </summary>
        /// <param></param>
        /// <returns>Sub Área removida</returns>
        /// <response code="200">Retorna sub área removida</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetailedReadSubAreaDTO>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value) as DetailedReadSubAreaDTO;
                _logger.LogInformation("Sub Área removida: {id}", model?.Id);
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