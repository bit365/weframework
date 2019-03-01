using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Domain.Users;

namespace WeFramework.Service.Users
{
    public interface IRoleService
    {
        Role GetRole(string roleName);

        Role GetRole(int roleID);

        IEnumerable<Role> GetRoles();

        void CreateRole(Role role);

        void UpdateRole(Role role);

        void DeleteRole(Role role);
    } 
}
