using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Entities.Security
{
    /// <summary>
    /// Represents a permission record
    /// </summary>
    public partial class PermissionRecord : BaseEntity
    {

        /// <summary>
        /// Gets or sets the permission name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the permission system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the permission category
        /// </summary>
        public string Category { get; set; }

        public bool IsActive { get; set; }

        //public virtual PermissionRole PermissionRole { get; set; }

        ///// <summary>
        ///// Gets or sets discount usage history
        ///// </summary>
        //public virtual ICollection<Role> Roles { get; set; }

    }
}
