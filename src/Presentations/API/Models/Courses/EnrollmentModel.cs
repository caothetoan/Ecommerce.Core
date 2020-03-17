using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnit.ApplicationCore.Entities.Courses;
using Vnit.ApplicationCore.Entities.Users;
using Vnit.WebFramework.Models;

namespace Catalog.API.Models.Courses
{
    public class EnrollmentModel:RootModel
    {
        public int CourseId { get; set; }

        public int UserId { get; set; }

        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }

        public virtual User User { get; set; }
    }
}
