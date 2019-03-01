using System.Collections.Generic;

namespace WeFramework.Core.Domain.Security
{
    public class DefaultPermission
    {
        public DefaultPermission()
        {
            this.Permissions = new List<Permission>();
        }

        public string RoleName { get; set; }


        public IEnumerable<Permission> Permissions { get; set; }
    }
}
