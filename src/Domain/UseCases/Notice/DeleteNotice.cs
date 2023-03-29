using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases.Notice;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.Notice
{
    public class DeleteNotice : IDeleteNotice
    {
        #region Global Scope
        private readonly INoticeRepository _repository;
        private readonly IMapper _mapper;
        public DeleteNotice(INoticeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadNoticeOutput> Execute(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var model = await _repository.Delete(id);
            return _mapper.Map<DetailedReadNoticeOutput>(model);
        }
    }
}