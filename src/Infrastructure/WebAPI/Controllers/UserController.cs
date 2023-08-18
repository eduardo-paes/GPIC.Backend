using Application.Ports.User;
using Application.Interfaces.UseCases.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Usuário.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        #region Global Scope
        private readonly IActivateUser _activateUser;
        private readonly IDeactivateUser _deactivateUser;
        private readonly IGetActiveUsers _getActiveUsers;
        private readonly IGetInactiveUsers _getInactiveUsers;
        private readonly IGetUserById _getUserById;
        private readonly IUpdateUser _updateUser;
        private readonly IMakeAdmin _makeAdmin;
        private readonly IMakeCoordinator _makeCoordinator;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Construtor do Controller de Usuário.
        /// </summary>
        /// <param name="activateUser">Ativa usuário.</param>
        /// <param name="deactivateUser">Desativa usuário.</param>
        /// <param name="getActiveUsers">Obtém todos os usuários ativos.</param>
        /// <param name="getInactiveUsers">Obtém todos os usuários inativos.</param>
        /// <param name="getUserById">Obtém usuário pelo id.</param>
        /// <param name="updateUser">Atualiza usuário.</param>
        /// <param name="makeAdmin">Torna usuário administrador.</param>
        /// <param name="makeCoordinator">Torna usuário coordenador.</param>
        /// <param name="logger">Logger.</param>
        public UserController(IActivateUser activateUser,
            IDeactivateUser deactivateUser,
            IGetActiveUsers getActiveUsers,
            IGetInactiveUsers getInactiveUsers,
            IGetUserById getUserById,
            IUpdateUser updateUser,
            IMakeAdmin makeAdmin,
            IMakeCoordinator makeCoordinator,
            ILogger<UserController> logger)
        {
            _activateUser = activateUser;
            _deactivateUser = deactivateUser;
            _getActiveUsers = getActiveUsers;
            _getInactiveUsers = getInactiveUsers;
            _getUserById = getUserById;
            _updateUser = updateUser;
            _makeAdmin = makeAdmin;
            _makeCoordinator = makeCoordinator;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Obtém usuário pelo id.
        /// </summary>
        /// <param name="id">Id do usuário.</param>
        /// <returns>Usuário encontrado.</returns>
        /// <response code="200">Usuário encontrado.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReadOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadOutput>> GetById(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _getUserById.ExecuteAsync(id);
                if (model == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Usuário encontrado para o ID {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Obtém todos os usuários ativos.
        /// </summary>
        /// <param name="skip">Quantidade de registros a serem ignorados.</param>
        /// <param name="take">Quantidade de registros a serem retornados.</param>
        /// <returns>Usuários ativos.</returns>
        /// <response code="200">Usuários encontrados.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Usuários não encontrados.</response>
        [HttpGet("Active/", Name = "GetAllActiveUsers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserReadOutput>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserReadOutput>>> GetAllActive(int skip = 0, int take = 50)
        {
            var models = await _getActiveUsers.ExecuteAsync(skip, take);
            if (!models.Any())
            {
                return NotFound("Nenhum usuário encontrado.");
            }
            _logger.LogInformation("Usuários encontrados: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Obtém todos os usuários inativos.
        /// </summary>
        /// <param name="skip">Quantidade de registros a serem ignorados.</param>
        /// <param name="take">Quantidade de registros a serem retornados.</param>
        /// <returns>Usuários inativos.</returns>
        /// <response code="200">Usuários encontrados.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Usuários não encontrados.</response>
        [HttpGet("Inactive/", Name = "GetAllInactiveUsers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserReadOutput>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserReadOutput>>> GetAllInactive(int skip = 0, int take = 50)
        {
            var models = await _getInactiveUsers.ExecuteAsync(skip, take);
            if (!models.Any())
            {
                return NotFound("Nenhum usuário encontrado.");
            }
            _logger.LogInformation("Usuários encontrados: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Atualiza usuário autenticado.
        /// </summary>
        /// <param name="request">Dados do usuário.</param>
        /// <returns>Usuário atualizado.</returns>
        /// <response code="200">Usuário atualizado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPut("{userId}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReadOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserReadOutput>> Update([FromBody] UserUpdateInput request)
        {
            try
            {
                var model = await _updateUser.ExecuteAsync(request);
                _logger.LogInformation("Usuário atualizado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Ativa usuário pelo Id.
        /// </summary>
        /// <param name="userId">Id do usuário.</param>
        /// <returns>Usuário ativado.</returns>
        /// <response code="200">Usuário ativado.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpPut("Active/{userId}", Name = "ActivateUser")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReadOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadOutput>> Activate(Guid? userId)
        {
            if (userId == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _activateUser.ExecuteAsync(userId.Value);
                _logger.LogInformation("Usuário ativado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Desativa usuário pelo Id.
        /// </summary>
        /// <param name="userId">Id do usuário.</param>
        /// <returns>Usuário desativado.</returns>
        /// <response code="200">Usuário desativado.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpPut("Inactive/{userId}", Name = "DeactivateUser")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReadOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadOutput>> Deactivate(Guid? userId)
        {
            if (userId == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var model = await _deactivateUser.ExecuteAsync(userId.Value);
                _logger.LogInformation("Usuário desativado: {id}", model?.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Torna usuário administrador pelo Id.
        /// </summary>
        /// <param name="userId">Id do usuário.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="200">Usuário administrador.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPut("Admin/{userId}", Name = "MakeAdmin")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReadOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserReadOutput>> MakeAdmin(Guid? userId)
        {
            if (userId == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var result = await _makeAdmin.ExecuteAsync(userId.Value);
                _logger.LogInformation("Operação realizada: {Resultado}", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Torna usuário coordenador pelo Id.
        /// </summary>
        /// <param name="userId">Id do usuário.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="200">Usuário coordenador.</response>
        /// <response code="400">Id não informado.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpPut("Coordinator/{userId}", Name = "MakeCoordinator")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReadOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserReadOutput>> MakeCoordinator(Guid? userId)
        {
            if (userId == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var result = await _makeCoordinator.ExecuteAsync(userId.Value);
                _logger.LogInformation("Operação realizada: {Resultado}", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
