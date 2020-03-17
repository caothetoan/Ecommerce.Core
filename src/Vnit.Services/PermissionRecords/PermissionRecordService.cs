using System;
using System.Collections.Generic;
using System.Linq;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Security;
using Vnit.ApplicationCore.Entities.Users;

namespace Vnit.ApplicationCore.Services.PermissionRecords
{
    public class PermissionRecordService : BaseEntityService<PermissionRecord>, IPermissionRecordService
    {
        private readonly IDataRepository<PermissionRole> _permissionRecordRoleDataRepository;
        //private readonly IWorkContext _workContext;

        public PermissionRecordService(IDataRepository<PermissionRecord> dataRepository,
            IDataRepository<PermissionRole> permissionRecordRoleDataRepository) : base(dataRepository)
        {
            _permissionRecordRoleDataRepository = permissionRecordRoleDataRepository;
        }



        public void AssignRoleToPermissionRecord(Role role, PermissionRecord permissionRecord)
        {
            var isAlreadyAssigned = GetPermissionRecords(role).Any(x => x.Id == permissionRecord.Id);
            if (isAlreadyAssigned)
                return;

            _permissionRecordRoleDataRepository.Insert(new PermissionRole()
            {
                RoleId = role.Id,
                PermissionRecordId = permissionRecord.Id
            });
        }
        public void AssignRoleToPermissionRecord(int permissionRecordId, Role role)
        {
            var permissionRecord =
                Repository.Get(
                        x => x.Id == permissionRecordId)
                    .FirstOrDefault();

            if (permissionRecord == null)
                throw new Exception(string.Format("The permissionRecord with name '{0}' can't be found", permissionRecordId));

            AssignRoleToPermissionRecord(role, permissionRecord);
        }

        public void AssignRoleToPermissionRecord(string permissionRecordSystemName, Role role)
        {
            var permissionRecord =
                Repository.Get(
                        x => string.Compare(x.SystemName, permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    .FirstOrDefault();

            if (permissionRecord == null)
                throw new Exception(string.Format("The permissionRecord with name '{0}' can't be found", permissionRecordSystemName));

            AssignRoleToPermissionRecord(role, permissionRecord);
        }

        public void UnassignRoleToPermission(Role role, PermissionRecord permissionRecord)
        {
            var permissionRecordRoleMapping = GetPermissionRecords(role).FirstOrDefault(x => x.Id == role.Id);
            if (permissionRecordRoleMapping == null)
                return;

            _permissionRecordRoleDataRepository.Delete(x => x.PermissionRecordId == permissionRecord.Id && x.RoleId == role.Id);

        }

        public void UnassignRoleToPermissionRecord(string roleName, Role role)
        {
            var permissionRecord = GetPermissionRecords(role).FirstOrDefault(x => x.SystemName == roleName);
            if (permissionRecord == null)
                return;

            _permissionRecordRoleDataRepository.Delete(x => x.PermissionRecordId == permissionRecord.Id && x.Role.SystemName == roleName);
        }

        public IList<PermissionRecord> GetPermissionRecords(int roleId)
        {
            var permissionRecordRoles = _permissionRecordRoleDataRepository.Get(x => x.RoleId == roleId).Select(x => x.PermissionRecord).ToList();
            return permissionRecordRoles;
        }

        public IList<PermissionRecord> GetPermissionRecords(Role role)
        {
            return GetPermissionRecords(role.Id);
        }


        ///// <summary>
        ///// Authorize permission
        ///// </summary>
        ///// <param name="permission">Permission record</param>
        ///// <returns>true - authorized; otherwise, false</returns>
        //public virtual bool Authorize(PermissionRecord permission)
        //{
        //    return Authorize(permission, _workContext.CurrentCustomer);
        //}

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(PermissionRecord permission, User customer)
        {
            if (permission == null)
                return false;

            if (customer == null)
                return false;
            
            return Authorize(permission.SystemName, customer);
        }

        ///// <summary>
        ///// Authorize permission
        ///// </summary>
        ///// <param name="permissionRecordSystemName">Permission record system name</param>
        ///// <returns>true - authorized; otherwise, false</returns>
        //public virtual bool Authorize(string permissionRecordSystemName)
        //{
        //    return Authorize(permissionRecordSystemName, _workContext.CurrentCustomer);
        //}

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string permissionRecordSystemName, User customer)
        {
            if (String.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            var customerRoles = customer.UserRoles;
            foreach (var userRole in customerRoles)
                if (Authorize(permissionRecordSystemName, userRole))
                    //yes, we have such permission
                    return true;

            //no permission found
            return false;
        }
        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="userRole">Customer role</param>
        /// <returns>true - authorized; otherwise, false</returns>
        protected virtual bool Authorize(string permissionRecordSystemName, UserRole userRole)
        {
            if (String.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            if (userRole == null)
                return false;
            if (userRole.Role == null)
                return false;
            var permissions = GetPermissionRecords(userRole.RoleId);
            {
                foreach (var permission1 in permissions)
                    if (permission1.SystemName.Equals(permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase))
                        return true;

                return false;
            };
        }

    }
}
