using Application.Ports.Student;
using Application.Interfaces.UseCases.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller de Estudante.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class StudentController : ControllerBase
    {
        #region Global Scope
        private readonly IGetStudentById _getStudentById;
        private readonly IGetStudentByRegistrationCode _getStudentByRegistrationCode;
        private readonly IGetStudents _getAllStudents;
        private readonly ICreateStudent _createStudent;
        private readonly IUpdateStudent _updateStudent;
        private readonly IDeleteStudent _deleteStudent;
        private readonly IRequestStudentRegister _requestStudentRegister;
        private readonly ILogger<StudentController> _logger;

        /// <summary>
        /// Construtor do Controller de Estudante.
        /// </summary>
        /// <param name="getStudentById">Obtém Estudante pelo id</param>
        /// <param name="getStudentByRegistrationCode">Obtém Estudante pela matrícula</param>
        /// <param name="getAllStudents">Obtém todos os Estudantes</param>
        /// <param name="createStudent">Cria Estudante</param>
        /// <param name="updateStudent">Atualiza Estudante</param>
        /// <param name="deleteStudent">Remove Estudante</param>
        /// <param name="requestStudentRegister">Solicita registro de Estudante</param>
        /// <param name="logger">Logger</param>
        public StudentController(IGetStudentById getStudentById,
            IGetStudentByRegistrationCode getStudentByRegistrationCode,
            IGetStudents getAllStudents,
            ICreateStudent createStudent,
            IUpdateStudent updateStudent,
            IDeleteStudent deleteStudent,
            IRequestStudentRegister requestStudentRegister,
            ILogger<StudentController> logger)
        {
            _getStudentById = getStudentById;
            _getStudentByRegistrationCode = getStudentByRegistrationCode;
            _getAllStudents = getAllStudents;
            _createStudent = createStudent;
            _updateStudent = updateStudent;
            _deleteStudent = deleteStudent;
            _requestStudentRegister = requestStudentRegister;
            _logger = logger;
        }
        #endregion Global Scope

        /// <summary>
        /// Busca Estudante pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante correspondente</returns>
        /// <response code="200">Retorna Estudante correspondente</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Estudante não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadStudentOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetailedReadStudentOutput>> GetById(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var student = await _getStudentById.ExecuteAsync(id.Value);
                if (student == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Estudante encontrado para o ID {id}.", id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca Estudante pelo id.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante correspondente</returns>
        /// <response code="200">Retorna Estudante correspondente</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Estudante não encontrado.</response>
        [HttpGet("RegistrationCode/{registrationCode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadStudentOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetailedReadStudentOutput>> GetByRegistrationCode(string? registrationCode)
        {
            if (string.IsNullOrWhiteSpace(registrationCode))
            {
                return BadRequest("A matrícula informada não pode ser nula ou vazia.");
            }

            try
            {
                var student = await _getStudentByRegistrationCode.ExecuteAsync(registrationCode);
                if (student == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Estudante encontrado para a matrícula {registrationCode}.", registrationCode);
                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Solicita registro de Estudante através do e-mail.
        /// </summary>
        /// <param name="email">E-mail do estudante</param>
        /// <returns>Informa se o envio do e-mail foi bem sucedido</returns>
        /// <response code="200">E-mail enviado com sucesso</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="500">Ocorreu um erro ao solicitar o registro do estudante.</response>
        [HttpPost("RequestRegister/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "ADMIN, PROFESSOR")]
        public async Task<ActionResult<string>> RequestStudentRegister(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("O e-mail informado não pode ser nulo ou vazio.");
            }

            try
            {
                string? message = await _requestStudentRegister.ExecuteAsync(email);
                if (string.IsNullOrWhiteSpace(message))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao solicitar o registro do estudante.");
                }
                _logger.LogInformation("{Message}.", message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Busca todas os Estudante ativos.
        /// </summary>
        /// <param></param>
        /// <returns>Todas os Estudante ativos</returns>
        /// <response code="200">Retorna todas os Estudante ativos</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhum Estudante encontrado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResumedReadStudentOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ResumedReadStudentOutput>>> GetAll(int skip = 0, int take = 50)
        {
            var students = await _getAllStudents.ExecuteAsync(skip, take);
            if (students == null || !students.Any())
            {
                return NotFound("Nenhum estudante encontrado.");
            }
            _logger.LogInformation("Estudantes encontrados: {quantidade}", students.Count());
            return Ok(students);
        }

        /// <summary>
        /// Cria Estudante.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante criado</returns>
        /// <response code="201">Retorna Estudante criado</response>
        /// <response code="400">Requisição incorreta.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailedReadStudentOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        public async Task<ActionResult<DetailedReadStudentOutput>> Create([FromBody] CreateStudentInput request)
        {
            try
            {
                var student = await _createStudent.ExecuteAsync(request);
                _logger.LogInformation("Estudante criado: {id}", student?.Id);
                return CreatedAtAction(nameof(GetById), new { id = student?.Id }, student);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza Estudante.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante atualizado</returns>
        /// <response code="200">Retorna Estudante atualizado</response>
        /// <response code="400">Requisição incorreta.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Nenhum Estudante encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadStudentOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "ADMIN, STUDENT")]
        public async Task<ActionResult<DetailedReadStudentOutput>> Update(Guid? id, [FromBody] UpdateStudentInput request)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var updatedStudent = await _updateStudent.ExecuteAsync(id.Value, request);
                if (updatedStudent == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Estudante atualizado: {id}", updatedStudent?.Id);
                return Ok(updatedStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove Estudante.
        /// </summary>
        /// <param></param>
        /// <returns>Estudante removido</returns>
        /// <response code="200">Retorna Estudante removido</response>
        /// <response code="400">Retorna mensagem de erro</response>
        /// <response code="401">Retorna mensagem de erro</response>
        /// <response code="404">Retorna mensagem de erro</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedReadStudentOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "ADMIN, STUDENT")]
        public async Task<ActionResult<DetailedReadStudentOutput>> Delete(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("O ID informado não pode ser nulo.");
            }

            try
            {
                var deletedStudent = await _deleteStudent.ExecuteAsync(id.Value);
                if (deletedStudent == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Estudante removido: {id}", deletedStudent?.Id);
                return Ok(deletedStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}