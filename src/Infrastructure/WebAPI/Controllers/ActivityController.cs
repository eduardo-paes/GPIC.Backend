using Application.Interfaces.UseCases.ActivityType;
using Application.Ports.Activity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar as atividades.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ActivityController : ControllerBase
    {
        private readonly IGetLastNoticeActivities _getLastNoticeActivities;
        private readonly IGetActivitiesByNoticeId _getActivitiesByNoticeId;
        private readonly ILogger<ActivityController> _logger;
        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="getLastNoticeActivities">Serviço de obtenção das últimas atividades em uso pelo edital anterior.</param>
        /// <param name="getActivitiesByNoticeId">Serviço de obtenção das atividades de um edital.</param>
        /// <param name="logger">Serviço de log.</param>
        public ActivityController(IGetLastNoticeActivities getLastNoticeActivities,
            IGetActivitiesByNoticeId getActivitiesByNoticeId,
            ILogger<ActivityController> logger)
        {
            _getLastNoticeActivities = getLastNoticeActivities;
            _getActivitiesByNoticeId = getActivitiesByNoticeId;
            _logger = logger;
        }

        /// <summary>
        /// Obtém as últimas atividades em uso pelo edital anterior.
        /// </summary>
        /// <returns>Lista de atividades mais recentes.</returns>
        /// <response code="200">Retorna a lista de atividades mais recentes.</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ActivityOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<IActionResult> GetLastNoticeActivities()
        {
            try
            {
                var result = await _getLastNoticeActivities.ExecuteAsync();
                _logger.LogInformation("Atividades encontradas.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtém as atividades de um edital.
        /// </summary>
        /// <param name="noticeId">Id do edital.</param>
        /// <returns>Lista de atividades.</returns>
        /// <response code="200">Retorna a lista de atividades.</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        [HttpGet("notice/{noticeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ActivityOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<IActionResult> GetActivitiesByNoticeId(Guid? noticeId)
        {
            if (noticeId == null)
            {
                return BadRequest("O ID do edital não pode ser nulo.");
            }

            try
            {
                var result = await _getActivitiesByNoticeId.ExecuteAsync(noticeId);
                _logger.LogInformation("Atividades encontradas para o edital {noticeId}.", noticeId);
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