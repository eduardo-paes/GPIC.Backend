using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Professor;
using Application.Ports.Professor;
using Application.Validation;

namespace Application.UseCases.Professor
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
        #endregion Global Scope

        public async Task<DetailedReadProfessorOutput> ExecuteAsync(Guid? id, UpdateProfessorInput model)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Recupera entidade que será atualizada
            var professor = await _professorRepository.GetByIdAsync(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.Professor));

            // Verifica se a entidade foi excluída
            if (professor.DeletedAt != null)
            {
                throw UseCaseException.BusinessRuleViolation("O professor informado já foi excluído.");
            }

            // Atualiza atributos permitidos
            professor.IdentifyLattes = model.IdentifyLattes;
            professor.SIAPEEnrollment = model.SIAPEEnrollment;

            // Atualiza professor com as informações fornecidas
            professor = await _professorRepository.UpdateAsync(professor);

            // Salva entidade atualizada no banco
            return _mapper.Map<DetailedReadProfessorOutput>(professor);
        }
    }
}