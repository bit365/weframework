using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeFramework.Web.Models.Users
{
    [DisplayName("用户")]
    public class UserModel
    {
        [DisplayName("编号")]
        public int ID { get; set; }

        [DisplayName("用户名")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("确认密码")]
        public string ConfirmPassword { get; set; }

        [DisplayName("创建时间")]
        public DateTime CreateDate { get; set; }

        [DisplayName("状态"), UIHint("Active")]
        public bool Active { get; set; }
    }
}