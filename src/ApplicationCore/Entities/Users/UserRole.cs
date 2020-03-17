using Vnit.ApplicationCore.Entities.Security;

namespace Vnit.ApplicationCore.Entities.Users
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        //public virtual User User { get; set; }
        
        public virtual Role Role { get; set; }
    }
}
