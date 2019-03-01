using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeFramework.Core.Domain.Navigates;
using WeFramework.Core.Domain.Security;
using WeFramework.Core.Domain.Users;
using WeFramework.Core.Infrastructure;
using WeFramework.Data;
using WeFramework.Service.Navigates;
using WeFramework.Service.Security;
using WeFramework.Service.Users;
using WeFramework.Web.Infrastructure;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(WeFramework.Web.App_Start.DataInitializer), "Start", Order = int.MaxValue)]
namespace WeFramework.Web.App_Start
{
    public static class DataInitializer
    {
        public static void Start()
        {
            string adminUserName = "admin";
            string adminRoleName = "系统管理员";
            string adminPassword = "admin";

            var permissionProviders = ServiceContainer.ResolveAll<IPermissionProvider>();

            var codePermissions = permissionProviders.SelectMany(pp => pp.GetPermissions());

            IPermissionService permissionService = ServiceContainer.Resolve<IPermissionService>();

            var dbPermissions = permissionService.GetPermissionsForTracking().ToList();

            //向数据库添加新增的权限列表

            foreach (var codePermission in codePermissions)
            {
                var dbPermission = dbPermissions.FirstOrDefault(dbp => dbp.Name == codePermission.Name);

                if (dbPermission == null)
                {
                    permissionService.CreatePermission(codePermission);
                }
                else
                {
                    dbPermission.Category = codePermission.Category;
                    dbPermission.Name = codePermission.Name;
                    dbPermission.Description = codePermission.Description;

                    permissionService.UpdatePermission(dbPermission);
                }
            }

            //从数据库删除无效的权限列表

            dbPermissions = permissionService.GetPermissionsForTracking().ToList();

            foreach (var dbPermission in dbPermissions)
            {
                var codePermission = codePermissions.SingleOrDefault(cp => cp.Name == dbPermission.Name);

                if (codePermission == null)
                {
                    permissionService.DeletePermission(dbPermission);
                }
            }

            //创建管理员用户、角色和权限

            IRoleService roleService = ServiceContainer.Resolve<IRoleService>();

            if (roleService.GetRole(adminRoleName) == null)
            {
                roleService.CreateRole(new Role { Name = adminRoleName, Active = true });
            }

            IUserService userService = ServiceContainer.Resolve<IUserService>();

            if (userService.GetUser(adminUserName) == null)
            {
                userService.CreateUser(new User
                {
                    Name = adminUserName,
                    Active = true,
                    Password = adminPassword,
                    CreateDate = DateTime.Now,
                    Remark = adminRoleName
                });
            }

            var adminRole = roleService.GetRole(adminRoleName);
            var adminUser = userService.GetUser(adminUserName);
            adminUser.Roles.Add(adminRole);

            userService.UpdateUser(adminUser);

            dbPermissions = permissionService.GetPermissionsForTracking().ToList();

            foreach (var permission in dbPermissions)
            {
                adminRole.Permissions.Add(permission);
            }

            roleService.UpdateRole(adminRole);

            var navigateService = ServiceContainer.Resolve<INavigateService>();

            var navigates = navigateService.GetNavigates();

            if (navigates == null || !navigates.Any())
            {
                var userInfoNavigate = new Navigate
                {
                    Name = "用户信息",
                    IconClassCode = "glyphicon glyphicon-user",
                    Active = true,
                    Children = new List<Navigate>
                    {
                        new Navigate { ControllerName = "Role", ActionName = "Index", Name = "角色管理", IconClassCode = "fa fa-group text-light-blue", Active = true },
                        new Navigate { ControllerName = "User", ActionName = "Index", Name = "用户管理", IconClassCode = "fa fa-user text-yellow", Active = true },
                        new Navigate { ControllerName = "User", ActionName = "ChangePassword", Name = "修改密码", IconClassCode = "fa fa-key text-light-blue", Active = true }
                    }
                };

                navigateService.CreateNavigate(userInfoNavigate);

                var sysInfoNavigate = new Navigate
                {
                    Name = "系统设置",
                    IconClassCode = "glyphicon glyphicon-cog",
                    Active = true,
                    Children = new List<Navigate>
                    {
                        new Navigate { ControllerName = "Navigate", ActionName = "Index", Name = "导航管理", IconClassCode = "fa fa-list text-green", Active = true },
                        new Navigate { ControllerName = "Log", ActionName = "Index", Name = "系统日志", IconClassCode = "fa fa-envelope-o", Active = true }
                    }
                };

                navigateService.CreateNavigate(sysInfoNavigate);

                var bllServiceNavigate = new Navigate
                {
                    Name = "系统业务",
                    IconClassCode = "fa fa-cube",
                    Active = true,
                    Children = new List<Navigate>
                    {
                        new Navigate { ControllerName = "Product", ActionName = "Index", Name = "产品管理", IconClassCode = "fa fa-flask text-light-blue", Active = true },
                    }
                };

                navigateService.CreateNavigate(bllServiceNavigate);
            }
        }
    }
}