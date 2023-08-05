using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.ProjectReport;
using Application.Ports.ProjectReport;
using Application.Validation;

namespace Application.UseCases.ProjectReport
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
            Domain.Entities.ProjectReport model = await _repository.DeleteAsync(id);

            // Retorna o curso removido
            return _mapper.Map<DetailedReadProjectReportOutput>(model);
        }
    }
}