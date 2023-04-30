using Domain.Contracts.Professor;
using Domain.Interfaces.UseCases.Professor;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.Professor
{
    public class DeleteProfessor : IDeleteProfessor
    {
        #region Global Scope
        private readonly IProfessorRepository _professorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public DeleteProfessor(IProfessorRepository professorRepository, IUserRepository userRepository, IMapper mapper)
        {
            _professorRepository = professorRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadProfessorOutput> Execute(Guid? id)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Verifica se o professor existe
            var professor = await _professorRepository.GetById(id);
            if (professor == null)
                throw new Exception("Professor não encontrado para o Id informado.");

            // Verifica se o usuário existe
            var user = await _userRepository.GetById(professor.UserId);
            if (user == null)
                throw new Exception("Usuário não encontrado para o Id informado.");

            // Remove o professor
            professor = await _professorRepository.Delete(id);
            if (professor == null)
                throw new Exception("O professor não pôde ser removido.");

            // Remove o usuário
            user = await _userRepository.Delete(professor.UserId);
            if (user == null)
                throw new Exception("O usuário não pôde ser removido.");

            // Retorna o professor removido
            return _mapper.Map<DetailedReadProfessorOutput>(professor);
        }
    }
}