using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de validação.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class Version : ControllerBase
    {
        /// <summary>
        /// Retorna a versão da API.
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(GetType()?.Assembly?.GetName()?.Version?.ToString() ?? "Versão não identificada.");
        }
    }
}