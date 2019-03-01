using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using WeFramework.Service.Logging;

namespace WeFramework.Web.Scheduler
{
    public class ClearLogJob : IJob
    {
        private readonly ILogService logService;

        public ClearLogJob(ILogService logService)
        {
            this.logService = logService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(()=> { logService.ClearLogs(); });
        }
    }
}