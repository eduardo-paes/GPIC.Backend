using Domain.Contracts.Student;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
        #endregion

        public async Task<DetailedReadStudentOutput> Execute(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se o estudante existe
            var student = await _studentRepository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Student));

            // Verifica se o usuário existe
            _ = await _userRepository.GetById(student.UserId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.User));

            // Remove o estudante
            student = await _studentRepository.Delete(id);
            UseCaseException.BusinessRuleViolation(student == null, "O estudante não pôde ser removido.");

            // Remove o usuário
            _ = await _userRepository.Delete(student?.UserId)
                ?? throw UseCaseException.BusinessRuleViolation("O usuário não pôde ser removido.");

            // Retorna o estudante removido
            return _mapper.Map<DetailedReadStudentOutput>(student);
        }
    }
}