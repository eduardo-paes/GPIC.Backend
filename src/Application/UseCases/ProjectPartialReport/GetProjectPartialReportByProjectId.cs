using Application.Interfaces.UseCases.ProjectPartialReport;
using Application.Ports.ProjectPartialReport;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Application.UseCases.ProjectPartialReport
{
    public class GetProjectPartialReportByProjectId : IGetProjectPartialReportByProjectId
    {
        #region Global Scope
        private readonly IProjectPartialReportRepository _repository;
        private readonly IMapper _mapper;
        public GetProjectPartialReportByProjectId(IProjectPartialReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadProjectPartialReportOutput> ExecuteAsync(Guid? projectId)
        {
            UseCaseException.NotInformedParam(projectId is null, nameof(projectId));
            var report = await _repository.GetByProjectIdAsync(projectId);
            return _mapper.Map<DetailedReadProjectPartialReportOutput>(report);
        }
    }
}