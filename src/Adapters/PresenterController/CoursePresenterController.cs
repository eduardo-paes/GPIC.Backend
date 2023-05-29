using Adapters.Gateways.Base;
using Adapters.Gateways.Course;
using Adapters.Interfaces;
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
        private readonly IGetCourses _getCoursees;
        private readonly IGetCourseById _getCourseById;

        public CoursePresenterController(ICreateCourse createCourse,
            IUpdateCourse updateCourse,
            IDeleteCourse deleteCourse,
            IGetCourses getCoursees,
            IGetCourseById getCourseById)
        {
            _createCourse = createCourse;
            _updateCourse = updateCourse;
            _deleteCourse = deleteCourse;
            _getCoursees = getCoursees;
            _getCourseById = getCourseById;
        }
        #endregion

        public async Task<IResponse?> Create(IRequest request) => await _createCourse.Execute((request as CreateCourseInput)!) as DetailedReadCourseResponse;
        public async Task<IResponse?> Delete(Guid? id) => await _deleteCourse.Execute(id) as DetailedReadCourseResponse;
        public async Task<IEnumerable<IResponse>?> GetAll(int skip, int take) => await _getCoursees.Execute(skip, take) as IEnumerable<ResumedReadCourseResponse>;
        public async Task<IResponse?> GetById(Guid? id) => await _getCourseById.Execute(id) as DetailedReadCourseResponse;
        public async Task<IResponse?> Update(Guid? id, IRequest request) => await _updateCourse.Execute(id, (request as UpdateCourseInput)!) as DetailedReadCourseResponse;
    }
}