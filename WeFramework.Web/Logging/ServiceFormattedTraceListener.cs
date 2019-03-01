using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System;
using System.Diagnostics;
using WeFramework.Core.Domain.Logging;
using WeFramework.Core.Infrastructure;
using WeFramework.Service.Logging;

namespace WeFramework.Web.Logging
{
    public class ServiceFormattedTraceListener : FormattedTraceListenerBase
    {
        public ServiceFormattedTraceListener(ILogFormatter formatter)
        {
            this.Formatter = formatter;
        }

        public override void Write(string message)
        {
            SaveLogEntry(new LogEntry
            {
                EventId = 0,
                Priority = 5,
                Severity = TraceEventType.Information,
                TimeStamp = DateTime.Now,
                Message = message,
            });
        }

        public override void WriteLine(string message)
        {
            throw new NotImplementedException();
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (data is LogEntry)
                {
                    SaveLogEntry(data as LogEntry);
                }
                else if (data is string)
                {
                    Write(data as string);
                }
                else
                {
                    base.TraceData(eventCache, source, eventType, id, data);
                }
            }
        }

        private void SaveLogEntry(LogEntry logEntry)
        {
            ServiceContainer.Resolve<ILogService>().CreateLog(new Log
            {
                EventID = logEntry.EventId,
                Priority = logEntry.Priority,
                Severity = logEntry.LoggedSeverity,
                Title = string.IsNullOrWhiteSpace(logEntry.Title) ? null : logEntry.Title.Trim(),
                Timestamp = logEntry.TimeStamp.ToLocalTime(),
                MachineName = logEntry.MachineName,
                Categories = string.Join(",", logEntry.CategoriesStrings),
                AppDomainName = logEntry.AppDomainName,
                ProcessID = logEntry.ProcessId,
                ProcessName = logEntry.ProcessName,
                ThreadName = logEntry.ManagedThreadName,
                ThreadId = logEntry.Win32ThreadId,
                Message = logEntry.Message,
                FormattedMessage = Formatter?.Format(logEntry)
            });
        }
    }
}