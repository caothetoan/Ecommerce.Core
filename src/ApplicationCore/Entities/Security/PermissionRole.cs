using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Entities.Security
{
    public class PermissionRole : BaseEntity
    {
        public int PermissionRecordId { get; set; }

        public int RoleId { get; set; }

        public virtual PermissionRecord PermissionRecord { get; set; }

        public virtual Role Role { get; set; }
    }
}
