using Microsoft.Practices.EnterpriseLibrary.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeFramework.Web.Scheduler
{
    public class SampleJob2 : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Logger.Write(context.JobDetail.Key.Name);
            return Task.FromResult(0);
        }
    }
}
