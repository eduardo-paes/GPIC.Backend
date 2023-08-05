using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.ProjectReport;
using Domain.UseCases.Ports.ProjectReport;
using Domain.Validation;

namespace Domain.UseCases.Interactors.ProjectReport
{
    public class DeleteProjectReport : IDeleteProjectReport
    {
        #region Global Scope
        private readonly IProjectReportRepository _repository;
        private readonly IMapper _mapper;
        public DeleteProjectReport(IProjectReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadProjectReportOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Remove a entidade
            Entities.ProjectReport model = await _repository.DeleteAsync(id);

            // Retorna o curso removido
            return _mapper.Map<DetailedReadProjectReportOutput>(model);
        }
    }
}