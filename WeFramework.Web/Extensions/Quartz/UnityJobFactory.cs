using Microsoft.Practices.Unity;
using Quartz;
using Quartz.Logging;
using Quartz.Simpl;
using Quartz.Spi;
using System;

namespace WeFramework.Web.Quartz
{
    public class UnityJobFactory : SimpleJobFactory
    {
        private static readonly ILog log = LogProvider.GetLogger(typeof(SimpleJobFactory));

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
                if (log.IsDebugEnabled())
                {
                    log.Debug($"Producing instance of jobkey={jobDetail.Key} class={jobType.FullName}");
                }

                return container.Resolve(jobType) as IJob ?? base.NewJob(bundle, scheduler);
            }
            catch (Exception e)
            {
                throw new SchedulerException($"Problem instantiating class {jobDetail.JobType.FullName}", e);
            }
        }
    }
}
