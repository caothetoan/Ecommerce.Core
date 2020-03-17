using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnit.ApplicationCore.Entities.Courses;
using Vnit.WebFramework.Models;

namespace Catalog.API.Models.Courses
{
    public class CourseModel :RootModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public string Thumbnail { get; set; }

        public int DisplayOrder { get; set; }

        public int Credits { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
