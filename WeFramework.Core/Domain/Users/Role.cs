using System.Collections.Generic;
using System.ComponentModel;
using WeFramework.Core.Domain.Common;
using WeFramework.Core.Domain.Security;

namespace WeFramework.Core.Domain.Users
{
    /// <summary>
    /// 系统角色
    /// </summary>
    public partial class Role:BaseEntity
    {
        public Role()
        {
            this.Permissions = new List<Permission>();
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DefaultValue(true)]
        public bool Active { get; set; }

        /// <summary>
        /// 角色拥有功能点
        /// </summary>
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
