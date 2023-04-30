using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
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
        #endregion

        public async Task<IQueryable<ResumedReadNoticeOutput>> Execute(int skip, int take)
        {
            var entities = await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadNoticeOutput>>(entities).AsQueryable();
        }
    }
}