using Unity.Lifetime;
using Quartz;
using Quartz.Logging;
using Quartz.Simpl;
using Quartz.Spi;
using System;
using Unity;
using WeFramework.Core.Domain.Logging;

namespace WeFramework.Web.Quartz
{
    public class UnityJobFactory : SimpleJobFactory
    {
        private readonly IUnityContainer container;

        public UnityJobFactory(IUnityContainer container)
        {
            this.container = container;
        }

        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            IJobDetail jobDetail = bundle.JobDetail;
            Type jobType = jobDetail.JobType;
            try
            {
                return container.Resolve(jobType) as IJob ?? base.NewJob(bundle, scheduler);
            }
            catch (Exception e)
            {
                throw new SchedulerException($"Problem instantiating class {jobDetail.JobType.FullName}", e);
            }
        }
    }
}
