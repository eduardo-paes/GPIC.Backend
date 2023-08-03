using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Notice;
using Domain.UseCases.Ports.Notice;

namespace Domain.UseCases.Interactors.Notice
{
    public class GetNotices : IGetNotices
    {
        #region Global Scope
        private readonly INoticeRepository _repository;
        private readonly IMapper _mapper;
        public GetNotices(INoticeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IEnumerable<ResumedReadNoticeOutput>> ExecuteAsync(int skip, int take)
        {
            IEnumerable<Entities.Notice> entities = (IEnumerable<Entities.Notice>)await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadNoticeOutput>>(entities);
        }
    }
}