using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Data;
using WeFramework.Core.Domain.Users;

namespace WeFramework.Service.Users
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> roleRepository;

        public RoleService(IRepository<Role> roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public void CreateRole(Role role)
        {
            roleRepository.Insert(role);
        }

        public void DeleteRole(Role role)
        {
            roleRepository.Delete(role);
        }

        public Role GetRole(int roleID)
        {
            return roleRepository.GetById(roleID);
        }

        public Role GetRole(string roleName)
        {
            return roleRepository.Table.SingleOrDefault(r => r.Name == roleName);
        }

        public IEnumerable<Role> GetRoles()
        {
            return roleRepository.Table;
        }

        public void UpdateRole(Role role)
        {
            roleRepository.Update(role);
        }
    }
}
