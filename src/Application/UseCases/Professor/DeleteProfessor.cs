using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Professor;
using Application.Ports.Professor;
using Application.Validation;

namespace Application.UseCases.Professor
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
        #endregion Global Scope

        public async Task<DetailedReadProfessorOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se o professor existe
            var professor = await _professorRepository.GetByIdAsync(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.Professor));

            // Verifica se o usuário existe
            _ = await _userRepository.GetByIdAsync(professor.UserId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.User));

            // Remove o professor
            professor = await _professorRepository.DeleteAsync(id);
            UseCaseException.BusinessRuleViolation(professor == null, "O professor não pôde ser removido.");

            // Remove o usuário
            _ = await _userRepository.DeleteAsync(professor?.UserId)
                ?? throw UseCaseException.BusinessRuleViolation("O usuário não pôde ser removido.");

            // Retorna o professor removido
            return _mapper.Map<DetailedReadProfessorOutput>(professor);
        }
    }
}