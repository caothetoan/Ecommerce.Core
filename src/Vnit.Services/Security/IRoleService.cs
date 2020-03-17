using System;
using System.Collections.Generic;
using System.Text;
using Vnit.ApplicationCore.Entities.Security;
using Vnit.ApplicationCore.Entities.Users;

namespace Vnit.ApplicationCore.Services.Security
{
    public interface IRoleService : IBaseEntityService<Role>
    {
        void AssignRoleToUser(Role role, User user);

        void AssignRoleToUser(string roleName, User user);

        void UnassignRoleToUser(Role role, User user);

        void UnassignRoleToUser(string roleName, User user);

        IList<Role> GetUserRoles(int userId);

        IList<Role> GetUserRoles(User user);

    }
}
