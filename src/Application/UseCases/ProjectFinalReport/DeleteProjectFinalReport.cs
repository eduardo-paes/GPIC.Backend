using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.ProjectFinalReport;
using Application.Ports.ProjectFinalReport;
using Application.Validation;

namespace Application.UseCases.ProjectFinalReport
{
    public class DeleteProjectFinalReport : IDeleteProjectFinalReport
    {
        #region Global Scope
        private readonly IProjectFinalReportRepository _repository;
        private readonly IMapper _mapper;
        public DeleteProjectFinalReport(IProjectFinalReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadProjectFinalReportOutput> ExecuteAsync(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));
            Domain.Entities.ProjectFinalReport model = await _repository.DeleteAsync(id);
            return _mapper.Map<DetailedReadProjectFinalReportOutput>(model);
        }
    }
}