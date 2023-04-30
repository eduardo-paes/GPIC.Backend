using Domain.Contracts.Student;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

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
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Verifica se o estudante existe
            var student = await _studentRepository.GetById(id);
            if (student == null)
                throw new Exception("Estudante não encontrado para o Id informado.");

            // Verifica se o usuário existe
            var user = await _userRepository.GetById(student.UserId);
            if (user == null)
                throw new Exception("Usuário não encontrado para o Id informado.");

            // Remove o estudante
            student = await _studentRepository.Delete(id);
            if (student == null)
                throw new Exception("O estudante não pôde ser removido.");

            // Remove o usuário
            user = await _userRepository.Delete(student.UserId);
            if (user == null)
                throw new Exception("O usuário não pôde ser removido.");

            // Retorna o estudante removido
            return _mapper.Map<DetailedReadStudentOutput>(student);
        }
    }
}