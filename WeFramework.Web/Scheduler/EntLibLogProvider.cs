using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuartzLogging = Quartz.Logging;
using EntLibLogging = Microsoft.Practices.EnterpriseLibrary.Logging;

namespace WeFramework.Web.Scheduler
{
    public class EntLibLogProvider : QuartzLogging.ILogProvider
    {
        public QuartzLogging.Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (func != null)
                {
                    EntLibLogging.Logger.Write(string.Format(func(), parameters));
                }
                return true;
            };
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }
    }
}