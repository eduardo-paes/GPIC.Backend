using Adapters.DTOs.Base;
using Adapters.DTOs.Notice;
using Adapters.Proxies.Notice;
using AutoMapper;
using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases.Notice;

namespace Adapters.Services
{
    public class NoticeService : INoticeService
    {
        #region Global Scope
        private readonly ICreateNotice _createNotice;
        private readonly IUpdateNotice _updateNotice;
        private readonly IDeleteNotice _deleteNotice;
        private readonly IGetNotices _getNotices;
        private readonly IGetNoticeById _getNoticeById;
        private readonly IMapper _mapper;

        public NoticeService(ICreateNotice createNotice, IUpdateNotice updateNotice, IDeleteNotice deleteNotice, IGetNotices getNotices, IGetNoticeById getNoticeById, IMapper mapper)
        {
            _createNotice = createNotice;
            _updateNotice = updateNotice;
            _deleteNotice = deleteNotice;
            _getNotices = getNotices;
            _getNoticeById = getNoticeById;
            _mapper = mapper;
        }
        #endregion

        public async Task<ResponseDTO> Create(RequestDTO model)
        {
            var dto = model as CreateNoticeDTO;
            var input = _mapper.Map<CreateNoticeInput>(dto);
            var result = await _createNotice.Execute(input);
            return _mapper.Map<DetailedReadNoticeDTO>(result);
        }

        public async Task<ResponseDTO> Delete(Guid? id)
        {
            var result = await _deleteNotice.Execute(id);
            return _mapper.Map<DetailedReadNoticeDTO>(result);
        }

        public async Task<IEnumerable<ResponseDTO>> GetAll(int skip, int take)
        {
            var result = await _getNotices.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadNoticeDTO>>(result);
        }

        public async Task<ResponseDTO> GetById(Guid? id)
        {
            var result = await _getNoticeById.Execute(id);
            return _mapper.Map<DetailedReadNoticeDTO>(result);
        }

        public async Task<ResponseDTO> Update(Guid? id, RequestDTO model)
        {
            var dto = model as UpdateNoticeDTO;
            var input = _mapper.Map<UpdateNoticeInput>(dto);
            var result = await _updateNotice.Execute(id, input);
            return _mapper.Map<DetailedReadNoticeDTO>(result);
        }
    }
}