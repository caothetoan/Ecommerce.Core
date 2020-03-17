using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Courses;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.Courses
{
   
    public class LessonService : BaseEntityService<Lesson>, ILessonService
    {
        public LessonService(IDataRepository<Lesson> dataRepository) : base(dataRepository)
        {
        }
    }
}
