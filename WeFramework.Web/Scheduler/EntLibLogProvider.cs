using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuartzLogging = Quartz.Logging;
using EntLibLogging = Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace WeFramework.Web.Scheduler
{
    public class EntLibLogProvider : QuartzLogging.ILogProvider
    {
        public QuartzLogging.Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (exception != null)
                {
                    return ExceptionPolicy.HandleException(exception, "quartzPolicy");
                }

                var logEntiry = new EntLibLogging.LogEntry();

                switch (level)
                {
                    case QuartzLogging.LogLevel.Trace:
                        logEntiry.Severity = System.Diagnostics.TraceEventType.Information;
                        break;
                    case QuartzLogging.LogLevel.Debug:
                        logEntiry.Severity = System.Diagnostics.TraceEventType.Verbose;
                        break;
                    case QuartzLogging.LogLevel.Info:
                        logEntiry.Severity = System.Diagnostics.TraceEventType.Information;
                        break;
                    case QuartzLogging.LogLevel.Warn:
                        logEntiry.Severity = System.Diagnostics.TraceEventType.Warning;
                        break;
                    case QuartzLogging.LogLevel.Error:
                        logEntiry.Severity = System.Diagnostics.TraceEventType.Error;
                        break;
                    case QuartzLogging.LogLevel.Fatal:
                        logEntiry.Severity = System.Diagnostics.TraceEventType.Error;
                        break;
                }

                if (func != null)
                {
                    logEntiry.Title = name;
                    logEntiry.Message = string.Format(func(), parameters);
                    EntLibLogging.Logger.Write(logEntiry);
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