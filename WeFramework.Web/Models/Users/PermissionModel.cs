using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeFramework.Web.Models.Users
{
    public class PermissionModel
    {
        /// <summary>
        /// 权限编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 隐含权限
        /// </summary>
        public IEnumerable<PermissionModel> Implies { get; set; }
    }
}