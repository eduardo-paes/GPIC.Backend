using Adapters.DTOs.Base;
using Adapters.DTOs.Course;
using Adapters.Proxies;
using AutoMapper;
using Domain.Contracts.Course;
using Domain.Interfaces.UseCases;

namespace Adapters.Services
{
    public class CourseService : ICourseService
    {
        #region Global Scope
        private readonly ICreateCourse _createCourse;
        private readonly IUpdateCourse _updateCourse;
        private readonly IDeleteCourse _deleteCourse;
        private readonly IGetCourses _getCourses;
        private readonly IGetCourseById _getCourseById;
        private readonly IMapper _mapper;

        public CourseService(ICreateCourse createCourse, IUpdateCourse updateCourse, IDeleteCourse deleteCourse, IGetCourses getCourses, IGetCourseById getCourseById, IMapper mapper)
        {
            _createCourse = createCourse;
            _updateCourse = updateCourse;
            _deleteCourse = deleteCourse;
            _getCourses = getCourses;
            _getCourseById = getCourseById;
            _mapper = mapper;
        }
        #endregion

        public async Task<ResponseDTO> Create(RequestDTO model)
        {
            var dto = model as CreateCourseDTO;
            var input = _mapper.Map<CreateCourseInput>(dto);
            var result = await _createCourse.Execute(input);
            return _mapper.Map<DetailedReadCourseDTO>(result);
        }

        public async Task<ResponseDTO> Delete(Guid? id)
        {
            var result = await _deleteCourse.Execute(id);
            return _mapper.Map<DetailedReadCourseDTO>(result);
        }

        public async Task<IEnumerable<ResponseDTO>> GetAll(int skip, int take)
        {
            var result = await _getCourses.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadCourseDTO>>(result);
        }

        public async Task<ResponseDTO> GetById(Guid? id)
        {
            var result = await _getCourseById.Execute(id);
            return _mapper.Map<DetailedReadCourseDTO>(result);
        }

        public async Task<ResponseDTO> Update(Guid? id, RequestDTO model)
        {
            var dto = model as UpdateCourseDTO;
            var input = _mapper.Map<UpdateCourseInput>(dto);
            var result = await _updateCourse.Execute(id, input);
            return _mapper.Map<DetailedReadCourseDTO>(result);
        }
    }
}