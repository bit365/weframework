using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFramework.Service.Users;
using WeFramework.Web.Core.Mvc;
using AutoMapper.QueryableExtensions;
using WeFramework.Web.Models.Users;
using WeFramework.Core.Domain.Users;
using WeFramework.Core.Paging;
using WeFramework.Service.Security;
using WeFramework.Web.Core.Security;

namespace WeFramework.Web.Controllers
{
    [ActionAuthorize]
    public class RoleController : BaseController
    {
        private readonly IConfigurationProvider mapperConfigProvider;

        private readonly IRoleService roleService;

        private readonly IPermissionService permissionService;

        private readonly IMapper mapper;

        public RoleController(IRoleService roleService, IPermissionService permissionService, IConfigurationProvider mapperConfigProvider)
        {
            this.roleService = roleService;
            this.permissionService = permissionService;
            this.mapperConfigProvider = mapperConfigProvider;
            this.mapper = mapperConfigProvider.CreateMapper();
        }

        public ActionResult Index()
        {
            return View(roleService.GetRoles().AsQueryable().ProjectTo<RoleModel>(mapperConfigProvider));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateValidFilter]
        public ActionResult Create(RoleModel model)
        {
            roleService.CreateRole(mapper.Map<Role>(model));
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(mapper.Map<RoleModel>(roleService.GetRole(id)));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateValidFilter]
        public ActionResult Edit(RoleModel model)
        {
            roleService.UpdateRole(mapper.Map(model, roleService.GetRole(model.ID)));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            roleService.DeleteRole(roleService.GetRole(id));
            return RedirectToAction("Index");
        }

        public ActionResult Authorize(int id)
        {
            Role role = roleService.GetRole(id);

            var groups = permissionService.GetPermissions().GroupBy(p => p.Category);

            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (var group in groups)
            {
                SelectListGroup selectListGroup = new SelectListGroup { Name = group.Key };
                selectListItems.AddRange(group.Select(g => new SelectListItem
                {
                    Group = selectListGroup,
                    Selected = role.Permissions.Any(rp => rp.ID == g.ID),
                    Value = g.ID.ToString(),
                    Text = g.Description
                }));
            }

            ViewBag.RoleName = role.Name;

            return View(new SelectList(selectListItems));
        }

        [HttpPost]
        public ActionResult Authorize(int id, IEnumerable<int> permissionIds)
        {
            Role role = roleService.GetRole(id);
            role.Permissions.ToList().ForEach(rp => role.Permissions.Remove(rp));
            permissionIds = permissionIds ?? Enumerable.Empty<int>();
            permissionIds.ToList().ForEach(pid => role.Permissions.Add(permissionService.GetPermission(pid)));
            roleService.UpdateRole(role);
            return RedirectToAction("Index");
        }
    }
}