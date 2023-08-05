using Application.Ports.Professor;
using Application.Interfaces.UseCases.Professor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Professor.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProfessorController : ControllerBase
    {
        #region Global Scope
        private readonly IGetProfessorById _getById;
        private readonly IGetProfessors _getAll;
        private readonly ICreateProfessor _create;
        private readonly IUpdateProfessor _update;
        private readonly IDeleteProfessor _delete;
        private readonly ILogger<ProfessorController> _logger;

        /// <summary>
        /// Construtor do Controller de Professor.
        /// </summary>
        /// <param name="getById">Serviço de obtenção de professor pelo id.</param>
        /// <param name="getAll">Serviço de obtenção de todos os professores ativos.</param>
        /// <param name="create">Serviço de criação de professor.</param>
        /// <param name="update">Serviço de atualização de professor.</param>
        /// <param name="delete">Serviço de remoção de professor.</param>
        /// <param name="logger">Serviço de log.</param>
        public ProfessorController(IGetProfessorById getById,
            IGetProfessors getAll,
            ICreateProfessor create,
            IUpdateProfessor update,
            IDeleteProfessor delete,
            ILogger<ProfessorController> logger)
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
        /// Busca Professor pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Professor correspondente</returns>
        /// <response code="200">Retorna Professor correspondente</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Professor não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProfessorOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetailedReadProfessorOutput>> GetById(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do professor não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var professor = await _getById.ExecuteAsync(id.Value);
                if (professor == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Professor encontrado para o ID {id}.", id);
                return Ok(professor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Busca todos os Professor ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todos os Professor ativos</returns>
        /// <response code="200">Retorna todos os Professor ativos</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhum Professor encontrado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResumedReadProfessorOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ResumedReadProfessorOutput>>> GetAll(int skip = 0, int take = 50)
        {
            var professors = await _getAll.ExecuteAsync(skip, take);
            if (professors == null || !professors.Any())
            {
                const string errorMessage = "Nenhum professor encontrado.";
                _logger.LogWarning(errorMessage);
                return NotFound(errorMessage);
            }
            _logger.LogInformation("Professores encontrados: {quantidade}", professors.Count());
            return Ok(professors);
        }

        /// <summary>
        /// Cria Professor.
        /// </summary>
        /// <param></param>
        /// <returns>Professor criado</returns>
        /// <response code="201">Retorna Professor criado</response>
        /// <response code="400">Requisição incorreta.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailedReadProfessorOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<DetailedReadProfessorOutput>> Create([FromBody] CreateProfessorInput request)
        {
            try
            {
                var createdProfessor = await _create.ExecuteAsync(request);
                _logger.LogInformation("Professor criado: {id}", createdProfessor?.Id);
                return CreatedAtAction(nameof(GetById), new { id = createdProfessor?.Id }, createdProfessor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza Professor.
        /// </summary>
        /// <param></param>
        /// <returns>Professor atualizado</returns>
        /// <response code="200">Retorna Professor atualizado</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Professor não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProfessorOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<DetailedReadProfessorOutput>> Update(Guid? id, [FromBody] UpdateProfessorInput request)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do professor não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var updatedProfessor = await _update.ExecuteAsync(id.Value, request);
                if (updatedProfessor == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Professor atualizado: {id}", updatedProfessor?.Id);
                return Ok(updatedProfessor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove Professor.
        /// </summary>
        /// <param></param>
        /// <returns>Professor removido</returns>
        /// <response code="200">Retorna Professor removido</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Professor não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadProfessorOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<DetailedReadProfessorOutput>> Delete(Guid? id)
        {
            if (id == null)
            {
                const string errorMessage = "O ID do professor não pode ser nulo.";
                _logger.LogWarning(errorMessage);
                return BadRequest(errorMessage);
            }

            try
            {
                var deletedProfessor = await _delete.ExecuteAsync(id.Value);
                if (deletedProfessor == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Professor removido: {id}", deletedProfessor?.Id);
                return Ok(deletedProfessor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}