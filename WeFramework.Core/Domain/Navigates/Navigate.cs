using System.Collections.Generic;
using WeFramework.Core.Domain.Common;

namespace WeFramework.Core.Domain.Navigates
{
    /// <summary>
    /// 菜单导航
    /// </summary>
    public partial class Navigate : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Navigate()
        {
            this.Children = new List<Navigate>();
        }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 行为名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 导航名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 导航图标
        /// </summary>
        public string IconClassCode { get; set; }

        /// <summary>
        /// 隶属导航编号
        /// </summary>
        public int? ParentID { get; set; }

        /// <summary>
        /// 隶属导航
        /// </summary>
        public Navigate Parent { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int? SortOrder { get; set; }

        /// <summary>
        /// 全部子菜单
        /// </summary>
        public virtual ICollection<Navigate> Children { get; set; }
    }
}
