using Application.Ports.ProgramType;
using Application.Interfaces.UseCases.ProgramType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Tipo de Programa.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProgramTypeController : ControllerBase
    {
        #region Global Scope
        private readonly IGetProgramTypeById _getById;
        private readonly IGetProgramTypes _getAll;
        private readonly ICreateProgramType _create;
        private readonly IUpdateProgramType _update;
        private readonly IDeleteProgramType _delete;
        private readonly ILogger<ProgramTypeController> _logger;

        /// <summary>
        /// Construtor do Controller de Tipo de Programa.
        /// </summary>
        /// <param name="getById">Serviço de obtenção de tipo de programa pelo id.</param>
        /// <param name="getAll">Serviço de obtenção de todos os tipos de programas ativos.</param>
        /// <param name="create">Serviço de criação de tipo de programa.</param>
        /// <param name="update">Serviço de atualização de tipo de programa.</param>
        /// <param name="delete">Serviço de remoção de tipo de programa.</param>
        /// <param name="logger">Serviço de log.</param>
        public ProgramTypeController(IGetProgramTypeById getById,
            IGetProgramTypes getAll,
            ICreateProgramType create,
            IUpdateProgramType update,
            IDeleteProgramType delete,
            ILogger<ProgramTypeController> logger)
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
        /// Busca tipo de programa pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Tipo de Programa correspondente</returns>
        /// <response code="200">Retorna Tipo de Programa correspondente</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Tipo de Programa não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProgramTypeOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<DetailedReadProgramTypeOutput>> GetById(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do tipo de programa não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var programType = await _getById.ExecuteAsync(id.Value);
                if (programType == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Tipo de programa encontrado para o ID {id}.", id);
                return Ok(programType);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas os tipos de programas ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todas os tipos de programas ativos</returns>
        /// <response code="200">Retorna todas os tipos de programas ativos</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhum tipo de programa encontrado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResumedReadProgramTypeOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<ResumedReadProgramTypeOutput>>> GetAll(int skip = 0, int take = 50)
        {
            var programTypes = await _getAll.ExecuteAsync(skip, take);
            if (programTypes == null || !programTypes.Any())
            {
                const string errorMessage = "Nenhum tipo de programa encontrado.";
                _logger.LogWarning(errorMessage);
                return NotFound(errorMessage);
            }
            _logger.LogInformation("Tipos de programa encontrados: {quantidade}", programTypes.Count());
            return Ok(programTypes);
        }

        /// <summary>
        /// Cria tipo de programa.
        /// </summary>
        /// <param></param>
        /// <returns>Tipo de Programa criado</returns>
        /// <response code="201">Retorna tipo de programa criado</response>
        /// <response code="400">Requisição incorreta.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailedReadProgramTypeOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadProgramTypeOutput>> Create([FromBody] CreateProgramTypeInput request)
        {
            try
            {
                var createdProgramType = await _create.ExecuteAsync(request);
                _logger.LogInformation("Tipo de programa criado: {id}", createdProgramType?.Id);
                return CreatedAtAction(nameof(GetById), new { id = createdProgramType?.Id }, createdProgramType);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza tipo de programa.
        /// </summary>
        /// <param></param>
        /// <returns>Tipo de Programa atualizado</returns>
        /// <response code="200">Retorna tipo de programa atualizado</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Tipo de Programa não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProgramTypeOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadProgramTypeOutput>> Update(Guid? id, [FromBody] UpdateProgramTypeInput request)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do tipo de programa não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var updatedProgramType = await _update.ExecuteAsync(id.Value, request);
                if (updatedProgramType == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Tipo de programa atualizado: {id}", updatedProgramType?.Id);
                return Ok(updatedProgramType);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove tipo de programa.
        /// </summary>
        /// <param></param>
        /// <returns>Tipo de Programa removido</returns>
        /// <response code="200">Retorna tipo de programa removido</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Tipo de Programa não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProgramTypeOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DetailedReadProgramTypeOutput>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do tipo de programa não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var deletedProgramType = await _delete.ExecuteAsync(id.Value);
                if (deletedProgramType == null)
                {
                    return NotFound("Nenhum registro encontrado.");
                }
                _logger.LogInformation("Tipo de programa removido: {id}", deletedProgramType?.Id);
                return Ok(deletedProgramType);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}