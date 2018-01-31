using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Domain.Security;
using WeFramework.Core.Domain.Users;

namespace WeFramework.Service.Security
{
    public interface IPermissionService
    {
        IEnumerable<Permission> GetPermissions();

        Permission GetPermission(int id);

        bool Authorize(Permission permission);

        bool Authorize(Permission permission, User user);

        bool Authorize(string permissionName);

        bool Authorize(string permissionName, User user);
    }
}
