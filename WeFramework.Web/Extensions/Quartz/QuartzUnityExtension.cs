using Microsoft.Practices.Unity;
using Quartz;
using System.Collections.Specialized;

namespace WeFramework.Web.Quartz
{
    public class QuartzUnityExtension : UnityContainerExtension
    {
        private readonly NameValueCollection quartzProperties;

        public QuartzUnityExtension(NameValueCollection quartzProperties)
        {
            this.quartzProperties = quartzProperties;
        }

        protected override void Initialize()
        {
            var constructor = new InjectionConstructor(new UnityJobFactory(Container), new InjectionParameter<NameValueCollection>(quartzProperties));

            this.Container.RegisterType<ISchedulerFactory, UnitySchedulerFactory>(new ContainerControlledLifetimeManager(), constructor);

            this.Container.RegisterType<IScheduler>(new InjectionFactory(c => c.Resolve<ISchedulerFactory>().GetScheduler().Result));
        }
    }
}
