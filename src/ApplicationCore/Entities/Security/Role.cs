using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Entities.Security
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }

        public bool IsSystemRole { get; set; }

        public string SystemName { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<PermissionRole> PermissionRoles { get; set; }

        ///// <summary>
        ///// Gets or sets discount usage history
        ///// </summary>
        //public virtual ICollection<PermissionRecord> PermissionRecords { get; set; }
    }
}
