using Domain.Contracts.SubArea;
using Domain.Interfaces.UseCases.SubArea;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.SubArea
{
    public class DeleteSubArea : IDeleteSubArea
    {
        #region Global Scope
        private readonly ISubAreaRepository _subAreaRepository;
        private readonly IMapper _mapper;
        public DeleteSubArea(ISubAreaRepository subAreaRepository, IMapper mapper)
        {
            _subAreaRepository = subAreaRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadSubAreaOutput> Execute(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var model = await _subAreaRepository.Delete(id);
            return _mapper.Map<DetailedReadSubAreaOutput>(model);
        }
    }
}