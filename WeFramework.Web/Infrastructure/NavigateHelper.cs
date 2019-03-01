using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Domain.Navigates;
using WeFramework.Core.Infrastructure;
using WeFramework.Service.Navigates;
using AutoMapper.QueryableExtensions;
using WeFramework.Web.Models.Navigates;
using AutoMapper;
using System.Web.Mvc;
using WeFramework.Service.Security;

namespace WeFramework.Web.Infrastructure
{
    public class NavigateHelper
    {
        public static IEnumerable<NavigateModel> GetCurrentUserActiveNavigates()
        {
            IEnumerable<NavigateModel> navigateModels = Enumerable.Empty<NavigateModel>();

            IWorkContext workContext = ServiceContainer.Resolve<IWorkContext>();

            INavigateService navigateService = ServiceContainer.Resolve<INavigateService>();

            if (workContext.CurrentUser != null)
            {
                var permissions = workContext.CurrentUser.Roles.SelectMany(r => r.Permissions);

                var config = new MapperConfiguration(cfg => cfg.CreateMap<Navigate, NavigateModel>().ForMember(m => m.Children, p => p.Ignore()).ForMember(m => m.Parent, p => p.Ignore()));

                navigateModels = navigateService.GetNavigates().AsQueryable().ProjectTo<NavigateModel>(config).ToList();

                foreach (var nav in navigateModels)
                {
                    nav.Parent = navigateModels.SingleOrDefault(n => n.ID == nav.ParentID);
                    nav.Children = navigateModels.Where(n => n.ParentID == nav.ID).ToList();
                    if (!string.IsNullOrWhiteSpace(nav.ControllerName) && !string.IsNullOrWhiteSpace(nav.ActionName))
                    {
                        nav.Active = permissions.Any(p => p.Name == nav.ControllerName + nav.ActionName);
                    }
                }
            }

            return navigateModels;
        }

        public static bool ContainsNavigateName(NavigateModel parentNavigate, string navigateName)
        {
            if (parentNavigate.Name == navigateName)
            {
                return true;
            }

            foreach (NavigateModel navigate in parentNavigate.Children.Where(nav => nav.Active))
            {
                if (ContainsNavigateName(navigate, navigateName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

