using AutoMapper;
using Unity.Lifetime;
using System;
using System.Linq;
using Unity;
using WeFramework.Core.Infrastructure;

namespace WeFramework.Web.Infrastructure
{
    public class AutoMapperRegistry : IDependencyRegister
    {
        public void RegisterTypes(IUnityContainer container)
        {
            var profileTypes = this.GetType().Assembly.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t));

            var profileInstances = profileTypes.Select(t => (Profile)Activator.CreateInstance(t));

            var config = new MapperConfiguration(cfg => { profileInstances.ToList().ForEach(p => cfg.AddProfile(p)); });

            container.RegisterInstance<IConfigurationProvider>(config);

            container.RegisterInstance<IMapper>(config.CreateMapper());
        }
    }
}
