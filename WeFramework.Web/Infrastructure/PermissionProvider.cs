using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using WeFramework.Core.Domain.Security;
using WeFramework.Service.Security;
using WeFramework.Web.Properties;

namespace WeFramework.Web.Infrastructure
{
    public class PermissionProvider : IPermissionProvider
    {

        #region 角色管理权限记录

        public static readonly Permission RoleList = new Permission { Name = "RoleIndex", Category = "角色管理", Description = "查看" };

        public static readonly Permission RoleCreate = new Permission { Name = "RoleCreate", Category = "角色管理", Description = "创建" };

        public static readonly Permission RoleEdit = new Permission { Name = "RoleEdit", Category = "角色管理", Description = "编辑" };

        public static readonly Permission RoleDelete = new Permission { Name = "RoleDelete", Category = "角色管理", Description = "删除" };

        public static readonly Permission RoleAuthorize = new Permission { Name = "RoleAuthorize", Category = "角色管理", Description = "授权" };

        #endregion

        #region 导航管理权限记录

        public static readonly Permission NavigateList = new Permission { Name = "NavigateIndex", Category = "导航管理", Description = "查看" };

        public static readonly Permission NavigateCreate = new Permission { Name = "NavigateCreate", Category = "导航管理", Description = "创建" };

        public static readonly Permission NavigateEdit = new Permission { Name = "NavigateEdit", Category = "导航管理", Description = "编辑" };

        public static readonly Permission NavigateDelete = new Permission { Name = "NavigateDelete", Category = "导航管理", Description = "删除" };

        #endregion

        #region 用户管理权限记录

        public static readonly Permission UserList = new Permission { Name = "UserIndex", Category = "用户管理", Description = "查看" };

        public static readonly Permission UserCreate = new Permission { Name = "UserCreate", Category = "用户管理", Description = "创建" };

        public static readonly Permission UserEdit = new Permission { Name = "UserEdit", Category = "用户管理", Description = "编辑" };

        public static readonly Permission UserDelete = new Permission { Name = "UserDelete", Category = "用户管理", Description = "删除" };

        public static readonly Permission UserChangePassword = new Permission { Name = "UserChangePassword", Category = "用户管理", Description = "更改密码" };

        #endregion

        #region 系统日志权限记录
        public static readonly Permission LogList = new Permission { Name = "LogIndex", Category = "运维管理", Description = "系统日志" };
        #endregion

        #region 产品相关权限记录
        public static readonly Permission ProductIndex = new Permission { Name = "ProductIndex", Category = "产品管理", Description = "查询" };
        public static readonly Permission ProductCreate = new Permission { Name = "ProductCreate", Category = "产品管理", Description = "创建" };

        #endregion

        public IEnumerable<Permission> GetPermissions()
        {
            var properties = this.GetType().GetFields().Where(t => t.FieldType == typeof(Permission));
            return properties.Select(p => p.GetValue(this) as Permission);
        }

        public IEnumerable<DefaultPermission> GetDefaultPermissions()
        {
            return new List<DefaultPermission>() {
                new DefaultPermission{ RoleName="系统管理员", Permissions=GetPermissions()}
            };
        }
    }
}