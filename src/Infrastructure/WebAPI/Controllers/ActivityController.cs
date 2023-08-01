using Adapters.Interfaces;
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
        private readonly IActivityPresenterController _activityPresenterController;
        private readonly ILogger<ActivityController> _logger;
        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="activityPresenterController"></param>
        /// <param name="logger"></param>
        public ActivityController(IActivityPresenterController activityPresenterController, ILogger<ActivityController> logger)
        {
            _activityPresenterController = activityPresenterController;
            _logger = logger;
        }

        /// <summary>
        /// Obtém as últimas atividades em uso pelo edital anterior.
        /// </summary>
        /// <returns>Lista de atividades mais recentes.</returns>
        /// <response code="200">Retorna a lista de atividades mais recentes.</response>
        /// <response code="400">Requisição incorreta.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLastNoticeActivities()
        {
            try
            {
                IEnumerable<Adapters.Gateways.Activity.ActivityTypeResponse> result = await _activityPresenterController.GetLastNoticeActivities();
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
        [HttpGet]
        [Route("notice/{noticeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetActivitiesByNoticeId(Guid? noticeId)
        {
            try
            {
                IEnumerable<Adapters.Gateways.Activity.ActivityTypeResponse> result = await _activityPresenterController.GetActivitiesByNoticeId(noticeId);
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