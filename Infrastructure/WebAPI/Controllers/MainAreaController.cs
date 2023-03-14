using Application.DTOs.MainArea;
using Application.Proxies.MainArea;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class MainAreaController : ControllerBase
    {
        #region Global Scope
        private readonly ICreateMainArea _createMainArea;
        private readonly IDeleteMainArea _deleteMainArea;
        private readonly IGetMainAreaById _getMainAreaById;
        private readonly IGetMainAreas _getMainAreas;
        private readonly IUpdateMainArea _updateMainArea;
        private readonly ILogger<MainAreaController> _logger;
        public MainAreaController(ICreateMainArea createMainArea, IDeleteMainArea deleteMainArea, IGetMainAreaById getMainAreaById, IGetMainAreas getMainAreas, IUpdateMainArea updateMainArea, ILogger<MainAreaController> logger)
        {
            _createMainArea = createMainArea;
            _deleteMainArea = deleteMainArea;
            _getMainAreaById = getMainAreaById;
            _getMainAreas = getMainAreas;
            _updateMainArea = updateMainArea;
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
        public async Task<ActionResult<ReadMainAreaDTO>> GetById(Guid? id)
        {
            if (id == null)
            {
                string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _getMainAreaById.Execute(id);
                _logger.LogInformation($"Área Principal encontrada para o id {id}.");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
        public async Task<ActionResult<IEnumerable<ReadMainAreaDTO>>> GetAll(int skip = 0, int take = 50)
        {
            var models = await _getMainAreas.Execute(skip, take);
            if (models == null)
            {
                string msg = "Nenhuma Área Principal encontrada.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation($"Áreas principais encontradas: {models.Count()}");
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
        public async Task<ActionResult<ReadMainAreaDTO>> Create([FromBody] CreateMainAreaDTO dto)
        {
            try
            {
                var model = await _createMainArea.Execute(dto);
                _logger.LogInformation($"Área principal criada: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
        public async Task<ActionResult<ReadMainAreaDTO>> Update(Guid? id, [FromBody] UpdateMainAreaDTO dto)
        {
            try
            {
                var model = await _updateMainArea.Execute(id, dto);
                _logger.LogInformation($"Área principal atualizada: {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
        public async Task<ActionResult<ReadMainAreaDTO>> Delete(Guid? id)
        {
            if (id == null)
            {
                string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _deleteMainArea.Execute(id.Value);
                _logger.LogInformation($"Área principal removida: {model.Id}");
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