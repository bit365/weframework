using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Domain.Common;
using WeFramework.Core.Domain.Users;

namespace WeFramework.Core.Domain.Security
{
    /// <summary>
    /// 系统实体权限
    /// </summary>
    public partial class EntityPermission
    {
        /// <summary>
        /// 实体编号
        /// </summary>
        public int EntityID { get; set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 权限角色
        /// </summary>
        public virtual Role Role { get; set; }
    }
}
