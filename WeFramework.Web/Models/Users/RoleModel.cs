using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeFramework.Web.Models.Users
{
    [DisplayName("角色")]
    public class RoleModel
    {
        [DisplayName("编号")]
        public int ID { get; set; }

        [DisplayName("名称")]
        public string Name { get; set; }

        [DisplayName("状态")]
        [UIHint("Active")]
        public bool Active { get; set; }
    }
}