using Adapters.DTOs.MainArea;
using Adapters.Proxies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    /// <summary>
    /// Controller de Área Principal.
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class MainAreaController : ControllerBase
    {
        #region Global Scope
        private readonly IMainAreaService _service;
        private readonly ILogger<MainAreaController> _logger;
        /// <summary>
        /// Construtor do Controller de Área Principal.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public MainAreaController(IMainAreaService service, ILogger<MainAreaController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca área principal pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Área principal correspondente</returns>
        /// <response code="200">Retorna área principal correspondente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedMainAreaDTO>> GetById(Guid? id)
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
                _logger.LogInformation("Área Principal encontrada para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas as áreas principais ativas.
        /// </summary>
        /// <param></param>
        /// <returns>Todas as áreas principais ativas</returns>
        /// <response code="200">Retorna todas as áreas principais ativas</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResumedReadMainAreaDTO>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _service.GetAll(skip, take);
            if (models == null)
            {
                const string msg = "Nenhuma Área Principal encontrada.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Áreas principais encontradas: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Cria área principal.
        /// </summary>
        /// <param></param>
        /// <returns>Área principal criada</returns>
        /// <response code="200">Retorna área principal criada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedMainAreaDTO>> Create([FromBody] CreateMainAreaDTO dto)
        {
            try
            {
                var model = await _service.Create(dto) as DetailedMainAreaDTO;
                _logger.LogInformation("Área principal criada: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza área principal.
        /// </summary>
        /// <param></param>
        /// <returns>Área principal atualizada</returns>
        /// <response code="200">Retorna área principal atualizada</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<DetailedMainAreaDTO>> Update(Guid? id, [FromBody] UpdateMainAreaDTO dto)
        {
            try
            {
                var model = await _service.Update(id, dto) as DetailedMainAreaDTO;
                _logger.LogInformation("Área principal atualizada: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove área principal.
        /// </summary>
        /// <param></param>
        /// <returns>Área principal removida</returns>
        /// <response code="200">Retorna área principal removida</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetailedMainAreaDTO>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.Delete(id.Value) as DetailedMainAreaDTO;
                _logger.LogInformation("Área principal removida: {id}", model?.Id);
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