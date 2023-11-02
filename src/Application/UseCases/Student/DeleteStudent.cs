using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Student;
using Application.Ports.Student;
using Application.Validation;

namespace Application.UseCases.Student
{
    public class DeleteStudent : IDeleteStudent
    {
        #region Global Scope
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public DeleteStudent(IStudentRepository studentRepository, IUserRepository userRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadStudentOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se o estudante existe
            Domain.Entities.Student? student = await _studentRepository.GetByIdAsync(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.Student));

            // Verifica se o usuário existe
            _ = await _userRepository.GetByIdAsync(student.UserId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.User));

            // Remove o estudante
            student = await _studentRepository.DeleteAsync(id);
            UseCaseException.BusinessRuleViolation(student == null, "O estudante não pôde ser removido.");

            // Remove o usuário
            _ = await _userRepository.DeleteAsync(student?.UserId)
                ?? throw UseCaseException.BusinessRuleViolation("O usuário não pôde ser removido.");

            // Retorna o estudante removido
            return _mapper.Map<DetailedReadStudentOutput>(student);
        }
    }
}