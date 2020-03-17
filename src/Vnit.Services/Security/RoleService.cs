﻿using System;
using System.Collections.Generic;
using System.Linq;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Security;
using Vnit.ApplicationCore.Entities.Users;

namespace Vnit.ApplicationCore.Services.Security
{
    public class RoleService : BaseEntityService<Role>, IRoleService
    {
        private readonly IDataRepository<UserRole> _userRoleDataRepository;

        public RoleService(IDataRepository<Role> dataRepository,
            IDataRepository<UserRole> userRoleDataRepository) : base(dataRepository)
        {
            _userRoleDataRepository = userRoleDataRepository;
        }

        public void AssignRoleToUser(Role role, User user)
        {
            var isAlreadyAssigned = GetUserRoles(user).Any(x => x.Id == role.Id);
            if (isAlreadyAssigned)
                return;

            _userRoleDataRepository.Insert(new UserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            });
        }

        public void AssignRoleToUser(string roleName, User user)
        {
            var role =
                Repository.Get(
                        x => string.Compare(x.SystemName, roleName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    .FirstOrDefault();

            if (role == null)
                throw new Exception(string.Format("The role with name '{0}' can't be found", roleName));

            AssignRoleToUser(role, user);
        }

        public void UnassignRoleToUser(Role role, User user)
        {
            var userRole = GetUserRoles(user).FirstOrDefault(x => x.Id == role.Id);
            if (userRole == null)
                return;

            _userRoleDataRepository.Delete(x => x.UserId == user.Id && x.RoleId == role.Id);

        }

        public void UnassignRoleToUser(string roleName, User user)
        {
            var userRole = GetUserRoles(user).FirstOrDefault(x => x.SystemName == roleName);
            if (userRole == null)
                return;

            _userRoleDataRepository.Delete(x => x.UserId == user.Id && x.Role.SystemName == roleName);
        }

        public IList<Role> GetUserRoles(int userId)
        {
            var userRoles = _userRoleDataRepository.Get(x => x.UserId == userId).Select(x => x.Role).ToList();
            return userRoles;
        }

        public IList<Role> GetUserRoles(User user)
        {
            return GetUserRoles(user.Id);
        }
    }
}
