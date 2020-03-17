using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Courses;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.Courses
{
   
    public class EnrollmentService : BaseEntityService<Enrollment>, IEnrollmentService
    {
        public EnrollmentService(IDataRepository<Enrollment> dataRepository) : base(dataRepository)
        {
        }
    }
}
