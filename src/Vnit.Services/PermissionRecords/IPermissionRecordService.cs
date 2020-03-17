using System.Collections.Generic;
using Vnit.ApplicationCore.Entities.Security;
using Vnit.ApplicationCore.Entities.Users;

namespace Vnit.ApplicationCore.Services.PermissionRecords
{
    public interface IPermissionRecordService : IBaseEntityService<PermissionRecord>
    {
        void AssignRoleToPermissionRecord(Role role, PermissionRecord permissionRecord);


        void AssignRoleToPermissionRecord(string permissionRecordSystemName, Role role);


        void UnassignRoleToPermission(Role role, PermissionRecord permissionRecord);

        void UnassignRoleToPermissionRecord(string roleName, Role role);

        IList<PermissionRecord> GetPermissionRecords(int roleId);

        IList<PermissionRecord> GetPermissionRecords(Role role);

        ///// <summary>
        ///// Authorize permission
        ///// </summary>
        ///// <param name="permission">Permission record</param>
        ///// <returns>true - authorized; otherwise, false</returns>
        //bool Authorize(PermissionRecord permission);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(PermissionRecord permission, User customer);

        ///// <summary>
        ///// Authorize permission
        ///// </summary>
        ///// <param name="permissionRecordSystemName">Permission record system name</param>
        ///// <returns>true - authorized; otherwise, false</returns>
        //bool Authorize(string permissionRecordSystemName);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(string permissionRecordSystemName, User customer);
    }
}
