using Catalog.API.Models.Courses;
using Vnit.ApplicationCore.Entities.Courses;
using Vnit.ApplicationCore.Helpers;

namespace Catalog.API.ModelExtensions
{
    public static class LessonExtension
    {
        public static LessonModel ToModel(this Lesson Lesson)
        {
            return Lesson.Map<LessonModel>();
        }

        public static Lesson ToEntity(this LessonModel Lesson)
        {
            return Lesson.Map<Lesson>();
        }
    }
}
