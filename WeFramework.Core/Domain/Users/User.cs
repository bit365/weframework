
using System;
using System.Collections.Generic;
using System.ComponentModel;
using WeFramework.Core.Domain.Common;

namespace WeFramework.Core.Domain.Users
{
    /// <summary>
    /// 系统用户信息
    /// </summary>
    public partial class User : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public User()
        {
            this.Roles = new List<Role>();
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 微信编号
        /// </summary>
        public string WeChatOpenID { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DefaultValue(true)]
        public bool Active { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 备注信息 
        /// </summary>
        public string Remark { get; set; }
    }
}
