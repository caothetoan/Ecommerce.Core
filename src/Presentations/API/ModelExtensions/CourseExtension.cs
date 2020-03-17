using Catalog.API.Models.Courses;
using Vnit.ApplicationCore.Entities.Courses;
using Vnit.ApplicationCore.Helpers;

namespace Catalog.API.ModelExtensions
{
    public static class CourseExtension
    {
        public static CourseModel ToModel(this Course Course)
        {
            return Course.Map<CourseModel>();
        }

        public static Course ToEntity(this CourseModel Course)
        {
            return Course.Map<Course>();
        }
    }
}
