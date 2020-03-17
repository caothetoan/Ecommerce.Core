using Catalog.API.Models.Courses;
using Vnit.ApplicationCore.Entities.Courses;
using Vnit.ApplicationCore.Helpers;

namespace Catalog.API.ModelExtensions
{
    public static class EnrollmentExtension
    {
        public static EnrollmentModel ToModel(this Enrollment Enrollment)
        {
            return Enrollment.Map<EnrollmentModel>();
        }

        public static Enrollment ToEntity(this EnrollmentModel Enrollment)
        {
            return Enrollment.Map<Enrollment>();
        }
    }
}
