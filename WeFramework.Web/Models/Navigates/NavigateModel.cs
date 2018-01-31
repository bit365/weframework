using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WeFramework.Core.Utility;

namespace WeFramework.Web.Models.Navigates
{
    [DisplayName("导航")]
    public class NavigateModel
    {
        public NavigateModel()
        {
            this.Children = new List<NavigateModel>();
        }

        [DisplayName("编号")]
        public int ID { get; set; }

        [DisplayName("控制器名称")]
        public string ControllerName { get; set; }

        [DisplayName("行为名称")]
        public string ActionName { get; set; }

        [DisplayName("导航名称")]
        public string Name { get; set; }

        [DisplayName("图标代码")]
        public string IconClassCode { get; set; }

        [DisplayName("序号")]
        public int? SortOrder { get; set; }

        [DisplayName("父级导航")]
        public NavigateModel Parent { get; set; }

        [DisplayName("父级导航编号")]
        public int? ParentID { get; set; }

        [DisplayName("子级菜单")]
        public virtual IEnumerable<NavigateModel> Children { get; set; }

        [DisplayName("状态")]
        [UIHint("Active")]
        public bool Active { get; set; }
    }
}
