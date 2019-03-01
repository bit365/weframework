using Unity.Lifetime;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeFramework.Core.Infrastructure;
using WeFramework.Service.Security;
using System.Reflection;
using System.Collections.Generic;

namespace WeFramework.Web.Core.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class ActionAuthorizeAttribute : AuthorizeAttribute
    {
        private static readonly string[] emptyArray = new string[0];

        private string[] permissionsSplit = emptyArray;

        public ActionAuthorizeAttribute(string permissions = null)
        {
            if (!String.IsNullOrEmpty(permissions))
            {
                permissionsSplit = permissions.Split(',').Select(str => str.Trim()).Where(trimmed => !string.IsNullOrEmpty(trimmed)).ToArray();
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }

            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            List<string> actionPermissionNames = permissionsSplit.ToList();

            actionPermissionNames.Add(string.Concat(controllerName, actionName));

            IPermissionService permissionService = ServiceContainer.Resolve<IPermissionService>();

            if (actionPermissionNames.Any(pName => permissionService.Authorize(pName)))
            {
                return;
            }

            HandleUnauthorizedRequest(filterContext);
        }

        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                throw new HttpException((int)HttpStatusCode.Forbidden, null);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
