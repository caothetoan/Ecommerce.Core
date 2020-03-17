using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Courses;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.Courses
{
   
    public class CourseService : BaseEntityService<Course>, ICourseService
    {
        public CourseService(IDataRepository<Course> dataRepository) : base(dataRepository)
        {
        }
    }
}
