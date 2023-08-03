using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Notice;
using Domain.UseCases.Ports.Notice;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Notice
{
    public class GetNoticeById : IGetNoticeById
    {
        #region Global Scope
        private readonly INoticeRepository _repository;
        private readonly IMapper _mapper;
        public GetNoticeById(INoticeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadNoticeOutput> ExecuteAsync(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));

            Entities.Notice? entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadNoticeOutput>(entity);
        }
    }
}