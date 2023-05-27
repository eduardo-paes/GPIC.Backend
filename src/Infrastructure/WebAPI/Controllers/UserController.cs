using System.Security.Claims;
using Adapters.Gateways.User;
using Adapters.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebAPI.Controllers
{
    /// <summary>
    /// Controller de Usuário.
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        #region Global Scope
        private readonly IUserPresenterController _service;
        private readonly ILogger<UserController> _logger;
        /// <summary>
        /// Construtor do Controller de Usuário.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public UserController(IUserPresenterController service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Busca usuário pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Todos os usuários ativos</returns>
        /// <response code="200">Retorna todos os usuários ativos</response>
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserReadResponse>>> GetById(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.GetUserById(id);
                _logger.LogInformation("Usuário encontrado para o id {id}.", id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca todos os usuários ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todos os usuários ativos</returns>
        /// <response code="200">Retorna todos os usuários ativos</response>
        [HttpGet("Active/", Name = "GetAllActiveUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserReadResponse>>> GetAllActive(int skip = 0, int take = 50)
        {
            var models = await _service.GetActiveUsers(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum usuário encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Usuários encontrados: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Busca todos os usuários inativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todos os usuários inativos</returns>
        /// <response code="200">Retorna todos os usuários ativos</response>
        [HttpGet("Inactive/", Name = "GetAllInactiveUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserReadResponse>>> GetAllInactive(int skip = 0, int take = 50)
        {
            var models = await _service.GetInactiveUsers(skip, take);
            if (models == null)
            {
                const string msg = "Nenhum usuário encontrado.";
                _logger.LogWarning(msg);
                return NotFound(msg);
            }
            _logger.LogInformation("Usuários encontrados: {quantidade}", models.Count());
            return Ok(models);
        }

        /// <summary>
        /// Realiza atualização do usuário logado.
        /// </summary>
        /// <param></param>
        /// <returns>Retorna usuário atualizado</returns>
        /// <response code="200">Retorna usuário atualizado</response>
        [HttpPut(Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserReadResponse>> Update([FromBody] UserUpdateRequest request)
        {
            try
            {
                // Recupera o usuário logado
                if (User.Identity is not ClaimsIdentity identity)
                {
                    const string msg = "Não foi possível recuperar o usuário logado.";
                    _logger.LogWarning(msg);
                    return BadRequest(msg);
                }

                // Recupera o id do usuário logado
                var idClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);
                if (idClaim == null)
                {
                    const string msg = "Não foi possível recuperar o id do usuário logado.";
                    _logger.LogWarning(msg);
                    return BadRequest(msg);
                }

                // Converte o id do usuário logado para Guid
                var idUser = Guid.Parse(idClaim.Value);

                // Atualiza o usuário e retorna o usuário atualizado
                var model = await _service.UpdateUser(idUser, request);

                _logger.LogInformation("Usuário atualizado: {id}", model.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza reativação de usário.
        /// </summary>
        /// <param></param>
        /// <returns>Retorna usuário reativado</returns>
        /// <response code="200">Retorna usuário reativado</response>
        [HttpPut("Active/{id}", Name = "ActivateUser")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserReadResponse>> Activate(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.ActivateUser(id.Value);
                _logger.LogInformation("Usuário ativado: {id}", model.Id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza desativação de usário.
        /// </summary>
        /// <param></param>
        /// <returns>Retorna usuário desativado</returns>
        /// <response code="200">Retorna usuário desativado</response>
        [HttpPut("Inactive/{id}", Name = "DeactivateUser")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserReadResponse>> Deactivate(Guid? id)
        {
            if (id == null)
            {
                const string msg = "O id informado não pode ser nulo.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            try
            {
                var model = await _service.DeactivateUser(id.Value);
                _logger.LogInformation("Usuário desativado: {id}", model.Id);
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