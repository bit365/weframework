using AutoMapper;
using System.Threading.Tasks;
using System.Web.Mvc;
using WeFramework.Service.Security;
using WeFramework.Service.Users;
using WeFramework.Web.Core.Mvc;
using WeFramework.Web.Models.Users;
using WeFramework.Web.Properties;
using WeFramework.Core.Paging;
using WeFramework.Core.Domain.Users;
using System.Collections.Generic;
using System;
using System.Linq;
using WeFramework.Core.Infrastructure;
using WeFramework.Web.Core.Security;

namespace WeFramework.Web.Controllers
{
    [ActionAuthorize]
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        private readonly IAuthorizeService authorizeService;

        private readonly IMapper mapper;

        private readonly IRoleService roleService;

        private readonly IEncryptionService encryptionService;

        private readonly IWorkContext workContext;

        public UserController(IUserService userService, IAuthorizeService authorizeService, IRoleService roleService, IMapper mapper, IEncryptionService encryptionService, IWorkContext workContext)
        {
            this.userService = userService;
            this.authorizeService = authorizeService;
            this.roleService = roleService;
            this.mapper = mapper;
            this.encryptionService = encryptionService;
            this.workContext = workContext;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ModelStateValidFilter]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (userService.ValidateUser(model.UserName, model.Password))
            {
                authorizeService.SignIn(userService.GetUser(model.UserName), model.RememberMe);
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, Resources.InvalidUserNameOrPassword);
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult SignOut()
        {
            authorizeService.SignOut();
            return RedirectToAction(nameof(Login));
        }

        public ActionResult Index(string keyword, int page = 1)
        {
            IPagedList<User> users = userService.GetUsers(keyword, page, 15);
            var userModels = mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(users);
            var viewModel = new StaticPagedList<UserModel>(userModels, users.GetMetaData());
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("UserListPartial", viewModel) : View(viewModel);
        }

        public ActionResult Create()
        {
            ViewBag.Roles = new SelectList(roleService.GetRoles(), "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel model, int[] roles)
        {
            if (ModelState.IsValid)
            {
                roles = roles ?? new int[0];
                User user = mapper.Map<UserModel, User>(model);
                user.CreateDate = DateTime.Now;
                Array.ForEach(roles, rid => user.Roles.Add(roleService.GetRole(rid)));
                userService.CreateUser(user);
                return RedirectToAction("Index");
            }
            ViewBag.Roles = new SelectList(roleService.GetRoles(), "ID", "Name");
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            User user = userService.GetUser(id);
            ViewBag.Roles = roleService.GetRoles().Select(r => new SelectListItem
            {
                Value = r.ID.ToString(),
                Text = r.Name,
                Selected = user.Roles.Any(ur => ur.ID == r.ID)
            });

            return View(mapper.Map<User, UserModel>(user, opts =>
            {
                opts.AfterMap((src, dest) =>
                {
                    dest.Password = DateTime.Now.ToShortDateString();
                    dest.ConfirmPassword = dest.Password;
                });
            }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model, int[] roles)
        {
            User user = userService.GetUser(model.ID);

            if (ModelState.IsValid)
            {
                roles = roles ?? new int[0];

                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, User>().ForMember(m => m.Password, p => p.Ignore()));

                config.CreateMapper().Map(model, user);

                if (model.Password != DateTime.Now.ToShortDateString())
                {
                    user.Password = encryptionService.HashPassword(model.Password);
                }
                Array.ForEach(user.Roles.ToArray(), r => user.Roles.Remove(roleService.GetRole(r.ID)));
                userService.UpdateUser(user);
                Array.ForEach(roles, rid => user.Roles.Add(roleService.GetRole(rid)));
                userService.UpdateUser(user);
                return RedirectToAction("Index");
            }
            ViewBag.Roles = roleService.GetRoles().Select(r => new SelectListItem
            {
                Value = r.ID.ToString(),
                Text = r.Name,
                Selected = user.Roles.Any(ur => ur.ID == r.ID)
            });
            return View(model);
        }


        [HttpPost]
        public ActionResult Delete(int[] check)
        {
            check = check ?? new int[0];
            Array.ForEach(check, id => userService.DeleteUser(id));
            return RedirectToAction("Index");
        }

        public ActionResult ChangePassword()
        {
            User user = workContext.CurrentUser;
            return View(mapper.Map<User, UserModel>(user, opts =>
            {
                opts.AfterMap((src, dest) =>
                {
                    dest.Password = DateTime.Now.ToShortDateString();
                    dest.ConfirmPassword = dest.Password;
                });
            }));
        }

        [HttpPost, ModelStateValidFilter]
        public ActionResult ChangePassword(UserModel userModel)
        {
            User user = userService.GetUser(userModel.ID);
            Mapper.Initialize(cfg => cfg.CreateMap<UserModel, User>().ForMember(m => m.CreateDate, p => p.Ignore()).ForMember(m => m.Password, p => p.Ignore()));
            Mapper.Map(userModel, user);

            if (userModel.Password != DateTime.Now.ToShortDateString())
            {
                user.Password = encryptionService.HashPassword(userModel.Password);
            }

            userService.UpdateUser(user);

            return RedirectToRoute("Default");
        }
    }
}
