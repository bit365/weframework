using System.Collections.Generic;
using WeFramework.Core.Domain.Security;

namespace WeFramework.Service.Security
{
    public interface IPermissionProvider
    {
        IEnumerable<Permission> GetPermissions();

        IEnumerable<DefaultPermission> GetDefaultPermissions();
    }
}
