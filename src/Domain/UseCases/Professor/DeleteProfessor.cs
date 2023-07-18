using Domain.Contracts.Professor;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se o professor existe
            var professor = await _professorRepository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Professor));

            // Verifica se o usuário existe
            _ = await _userRepository.GetById(professor.UserId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.User));

            // Remove o professor
            professor = await _professorRepository.Delete(id);
            UseCaseException.BusinessRuleViolation(professor == null, "O professor não pôde ser removido.");

            // Remove o usuário
            _ = await _userRepository.Delete(professor?.UserId)
                ?? throw UseCaseException.BusinessRuleViolation("O usuário não pôde ser removido.");

            // Retorna o professor removido
            return _mapper.Map<DetailedReadProfessorOutput>(professor);
        }
    }
}