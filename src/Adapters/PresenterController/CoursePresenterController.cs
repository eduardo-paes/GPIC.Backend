using Adapters.Gateways.Base;
using Adapters.Gateways.Course;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Ports.Course;
using Domain.UseCases.Interfaces.Course;
using Domain.UseCases.Ports.Course;

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
        #endregion Global Scope

        public async Task<IResponse> Create(IRequest request)
        {
            CreateCourseRequest? dto = request as CreateCourseRequest;
            CreateCourseInput input = _mapper.Map<CreateCourseInput>(dto);
            DetailedReadCourseOutput result = await _createCourse.ExecuteAsync(input);
            return _mapper.Map<DetailedReadCourseResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            DetailedReadCourseOutput result = await _deleteCourse.ExecuteAsync(id);
            return _mapper.Map<DetailedReadCourseResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            IQueryable<ResumedReadCourseOutput> result = await _getCourses.ExecuteAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadCourseResponse>>(result);
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            DetailedReadCourseOutput result = await _getCourseById.ExecuteAsync(id);
            return _mapper.Map<DetailedReadCourseResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            UpdateCourseRequest? dto = request as UpdateCourseRequest;
            UpdateCourseInput input = _mapper.Map<UpdateCourseInput>(dto);
            DetailedReadCourseOutput result = await _updateCourse.ExecuteAsync(id, input);
            return _mapper.Map<DetailedReadCourseResponse>(result);
        }
    }
}