using Quartz;
using Quartz.Core;
using Quartz.Impl;
using System.Collections.Specialized;

namespace WeFramework.Web.Quartz
{
    public class UnitySchedulerFactory : StdSchedulerFactory
    {
        private readonly UnityJobFactory unityJobFactory;

        public UnitySchedulerFactory(UnityJobFactory unityJobFactory, NameValueCollection quartzProperties = null)
        {
            if (quartzProperties != null)
            {
                base.Initialize(quartzProperties);
            }

            this.unityJobFactory = unityJobFactory;
        }

        protected override IScheduler Instantiate(QuartzSchedulerResources schedulerResources, QuartzScheduler scheduler)
        {
            scheduler.JobFactory = this.unityJobFactory;
            return base.Instantiate(schedulerResources, scheduler);
        }
    }
}
