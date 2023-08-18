using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.ProjectFinalReport;
using Application.Ports.ProjectFinalReport;
using Application.Validation;

namespace Application.UseCases.ProjectFinalReport
{
    public class GetProjectFinalReportById : IGetProjectFinalReportById
    {
        #region Global Scope
        private readonly IProjectFinalReportRepository _repository;
        private readonly IMapper _mapper;
        public GetProjectFinalReportById(IProjectFinalReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadProjectFinalReportOutput> ExecuteAsync(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));
            Domain.Entities.ProjectFinalReport? entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadProjectFinalReportOutput>(entity);
        }
    }
}