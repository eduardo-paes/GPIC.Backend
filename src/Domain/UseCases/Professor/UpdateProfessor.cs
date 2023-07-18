using Domain.Contracts.Professor;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
{
    public class UpdateProfessor : IUpdateProfessor
    {
        #region Global Scope
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;
        public UpdateProfessor(IProfessorRepository professorRepository, IMapper mapper)
        {
            _professorRepository = professorRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadProfessorOutput> Execute(Guid? id, UpdateProfessorInput input)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Recupera entidade que será atualizada
            var professor = await _professorRepository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Professor));

            // Verifica se a entidade foi excluída
            if (professor.DeletedAt != null)
                throw UseCaseException.BusinessRuleViolation("O professor informado já foi excluído.");

            // Atualiza atributos permitidos
            professor.IdentifyLattes = input.IdentifyLattes;
            professor.SIAPEEnrollment = input.SIAPEEnrollment;

            // Atualiza professor com as informações fornecidas
            professor = await _professorRepository.Update(professor);

            // Salva entidade atualizada no banco
            return _mapper.Map<DetailedReadProfessorOutput>(professor);
        }
    }
}