using Adapters.Gateways.Base;
using Adapters.Gateways.Course;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.Course;
using Domain.Interfaces.UseCases;

namespace Adapters.PresenterController
{
    public class CoursePresenterController : ICoursePresenterController
    {
        #region Global Scope
        private readonly ICreateCourse _createCourse;
        private readonly IUpdateCourse _updateCourse;
        private readonly IDeleteCourse _deleteCourse;
        private readonly IGetCourses _getCourses;
        private readonly IGetCourseById _getCourseById;
        private readonly IMapper _mapper;

        public CoursePresenterController(ICreateCourse createCourse, IUpdateCourse updateCourse, IDeleteCourse deleteCourse, IGetCourses getCourses, IGetCourseById getCourseById, IMapper mapper)
        {
            _createCourse = createCourse;
            _updateCourse = updateCourse;
            _deleteCourse = deleteCourse;
            _getCourses = getCourses;
            _getCourseById = getCourseById;
            _mapper = mapper;
        }
        #endregion

        public async Task<Response> Create(Request request)
        {
            var dto = request as CreateCourseRequest;
            var input = _mapper.Map<CreateCourseInput>(dto);
            var result = await _createCourse.Execute(input);
            return _mapper.Map<DetailedReadCourseResponse>(result);
        }

        public async Task<Response> Delete(Guid? id)
        {
            var result = await _deleteCourse.Execute(id);
            return _mapper.Map<DetailedReadCourseResponse>(result);
        }

        public async Task<IEnumerable<Response>> GetAll(int skip, int take)
        {
            var result = await _getCourses.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadCourseResponse>>(result);
        }

        public async Task<Response> GetById(Guid? id)
        {
            var result = await _getCourseById.Execute(id);
            return _mapper.Map<DetailedReadCourseResponse>(result);
        }

        public async Task<Response> Update(Guid? id, Request request)
        {
            var dto = request as UpdateCourseRequest;
            var input = _mapper.Map<UpdateCourseInput>(dto);
            var result = await _updateCourse.Execute(id, input);
            return _mapper.Map<DetailedReadCourseResponse>(result);
        }
    }
}