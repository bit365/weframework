using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using UnityMvc = Unity.AspNet.Mvc;
using UnityWebApi = Unity.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WeFramework.Web.App_Start.UnityWebActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(WeFramework.Web.App_Start.UnityWebActivator), "Shutdown")]

namespace WeFramework.Web.App_Start
{
    /// <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    public static class UnityWebActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start()
        {
            var container = UnityConfig.GetConfiguredContainer();

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityMvc.UnityFilterAttributeFilterProvider(container));

            DependencyResolver.SetResolver(new UnityMvc.UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityWebApi.UnityDependencyResolver(container);

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityMvc.UnityPerRequestHttpModule));
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}