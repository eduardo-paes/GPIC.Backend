using Domain.Contracts.Professor;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

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

        public async Task<DetailedReadProfessorOutput> Execute(Guid? id, UpdateProfessorInput dto)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Recupera entidade que será atualizada
            var professor = await _professorRepository.GetById(id);

            // Verifica se o professor foi encontrado
            if (professor == null)
                throw new Exception("Nenhum professor encontrado para o Id informado.");

            // Verifica se a entidade foi excluída
            if (professor.DeletedAt != null)
                throw new Exception("O professor informado já foi excluído.");

            // Atualiza atributos permitidos
            professor.IdentifyLattes = dto.IdentifyLattes;
            professor.SIAPEEnrollment = dto.SIAPEEnrollment;

            // Atualiza professor com as informações fornecidas
            professor = await _professorRepository.Update(professor);

            // Salva entidade atualizada no banco
            return _mapper.Map<DetailedReadProfessorOutput>(professor);
        }
    }
}