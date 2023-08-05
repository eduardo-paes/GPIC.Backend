using Application.Ports.SubArea;
using Application.Interfaces.UseCases.SubArea;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Sub Área.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class SubAreaController : ControllerBase
    {
        #region Global Scope
        private readonly IGetSubAreaById _getSubAreaById;
        private readonly IGetSubAreasByArea _getSubAreasByArea;
        private readonly ICreateSubArea _createSubArea;
        private readonly IUpdateSubArea _updateSubArea;
        private readonly IDeleteSubArea _deleteSubArea;
        private readonly ILogger<SubAreaController> _logger;

        /// <summary>
        /// Construtor do Controller de Sub Área.
        /// </summary>
        /// <param name="getSubAreaById">Obtém sub área pelo id.</param>
        /// <param name="getSubAreasByArea">Obtém todas as sub áreas ativas da área.</param>
        /// <param name="createSubArea">Cria sub área.</param>
        /// <param name="updateSubArea">Atualiza sub área.</param>
        /// <param name="deleteSubArea">Remove sub área.</param>
        /// <param name="logger">Logger.</param>
        public SubAreaController(IGetSubAreaById getSubAreaById,
            IGetSubAreasByArea getSubAreasByArea,
            ICreateSubArea createSubArea,
            IUpdateSubArea updateSubArea,
            IDeleteSubArea deleteSubArea,
            ILogger<SubAreaController> logger)
        {
            _getSubAreaById = getSubAreaById;
            _getSubAreasByArea = getSubAreasByArea;
            _createSubArea = createSubArea;
            _updateSubArea = updateSubArea;
            _deleteSubArea = deleteSubArea;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Obtém sub área pelo id.
        /// </summary>
        /// <param name="id">Id da sub área.</param>
        /// <returns>Sub área.</returns>
        /// <response code="200">Sub área encontrada.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Sub área não encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadSubAreaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetailedReadSubAreaOutput>> GetById(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _getSubAreaById.ExecuteAsync(id);
                if (model == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Sub Área encontrada para o ID {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Obtém todas as sub áreas ativas da área.
        /// </summary>
        /// <param name="areaId">Id da área.</param>
        /// <param name="skip">Quantidade de registros a serem ignorados.</param>
        /// <param name="take">Quantidade de registros a serem retornados.</param>
        /// <returns>Sub áreas.</returns>
        /// <response code="200">Sub áreas encontradas.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Sub áreas não encontradas.</response>
        [HttpGet("area/{areaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResumedReadSubAreaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ResumedReadSubAreaOutput>>> GetSubAreasByArea(Guid? areaId, int skip = 0, int take = 50)
        {
            if (areaId == null)
            {
                return BadRequest("O ID da área informado não pode ser nulo.");
            }

            try
            {
                var models = await _getSubAreasByArea.ExecuteAsync(areaId, skip, take);
                if (!models.Any())
                {
                    return NotFound("Nenhuma Sub Área encontrada.");
                }
                _logger.LogInformation("Sub Áreas encontradas: {quantidade}", models.Count());
                return Ok(models);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cria sub área.
        /// </summary>
        /// <param name="request">Dados da sub área.</param>
        /// <returns>Sub área criada.</returns>
        /// <response code="201">Sub área criada.</response>
        /// <response code="400">Dados da sub área inválidos.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailedReadSubAreaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadSubAreaOutput>> Create([FromBody] CreateSubAreaInput request)
        {
            try
            {
                var model = await _createSubArea.ExecuteAsync(request);
                _logger.LogInformation("Sub Área criada: {id}", model?.Id);
                return CreatedAtAction(nameof(GetById), new { id = model?.Id }, model);
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
        /// <param name="id">Id da sub área.</param>
        /// <param name="request">Dados da sub área.</param>
        /// <returns>Sub área atualizada.</returns>
        /// <response code="200">Sub área atualizada.</response>
        /// <response code="400">Dados da sub área inválidos.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Sub área não encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadSubAreaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadSubAreaOutput>> Update(Guid? id, [FromBody] UpdateSubAreaInput request)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _updateSubArea.ExecuteAsync(id.Value, request);
                if (model == null)
                {
                    return NotFound();
                }
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
        /// <param name="id">Id da sub área.</param>
        /// <returns>Sub área removida.</returns>
        /// <response code="200">Sub área removida.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Sub área não encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadSubAreaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadSubAreaOutput>> Delete(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _deleteSubArea.ExecuteAsync(id.Value);
                if (model == null)
                {
                    return NotFound();
                }
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
