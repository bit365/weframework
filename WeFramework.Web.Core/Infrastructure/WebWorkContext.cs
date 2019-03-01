using Unity.Lifetime;
using Unity;
using WeFramework.Core.Domain.Users;
using WeFramework.Core.Infrastructure;
using WeFramework.Service.Security;

namespace WeFramework.Web.Core.Infrastructure
{
    public class WebWorkContext : IWorkContext
    {
        private readonly IUnityContainer container = null;

        public WebWorkContext(IUnityContainer unityContainer)
        {
            this.container = unityContainer;
        }

        public IUnityContainer Container
        {
            get { return this.container; }
        }

        public User CurrentUser
        {
            get { return container.Resolve<IAuthorizeService>().GetAuthorizedUser(); }
        }
    }
}
