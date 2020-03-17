using System;
using Vnit.ApplicationCore.Attributes;
using Vnit.ApplicationCore.Entities.Users;

namespace Vnit.ApplicationCore.Entities.Courses
{
    public class Assessment: BaseEntity
    {
        #region Propertises
        public int UserId { get; set; }

        public int CourseId { get; set; }

        [TokenField]
        public decimal Point { get; set; }

        [TokenField]
        public DateTime StartOnUtc { get; set; }

        [TokenField]
        public DateTime? EndOnUtc { get; set; }

        [TokenField]
        public int Duration { get; set; }

        public int AllowDuration { get; set; }


        public bool Finished { get; set; }

        [TokenField]
        public string Name { get; set; }

        [TokenField]
        public string Body { get; set; } 
        #endregion

        public virtual  User User { get; set; }

        public virtual Course Course { get; set; }
    }
}
