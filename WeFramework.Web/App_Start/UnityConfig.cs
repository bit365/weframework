using Microsoft.Practices.Unity;
using System;
using System.Configuration;
using Unity;
using WeFramework.Core.Configuration;
using WeFramework.Core.Infrastructure;
using WeFramework.Web.Core.Infrastructure;

namespace WeFramework.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            RegisterTypes(ServiceContainer.Current);
            return ServiceContainer.Current;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            //container.RegisterType<IProductRepository, ProductRepository>();

            Configure(container);
        }

        public static void Configure(IUnityContainer container)
        {
            string configurationFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Unity.config");
            var configurationFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configurationFilePath };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(configurationFileMap, ConfigurationUserLevel.None);
            var section = (Microsoft.Practices.Unity.Configuration.UnityConfigurationSection)configuration.GetSection("unity");
            section.Configure(container);

            var config = ConfigurationManager.GetSection("applicationConfig") as ApplicationConfig;
            container.RegisterInstance<ApplicationConfig>(config);

            ITypeFinder typeFinder = new WebTypeFinder();
            container.RegisterInstance<ITypeFinder>(typeFinder);
            var registerTypes = typeFinder.FindClassesOfType<IDependencyRegister>();
            foreach (Type registerType in registerTypes)
            {
                var register = (IDependencyRegister)Activator.CreateInstance(registerType);
                register.RegisterTypes(container);
            }
        }
    }
}
