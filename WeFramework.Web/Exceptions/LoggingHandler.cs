using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WeFramework.Web.Exceptions
{
    [ConfigurationElementType(typeof(LoggingHandlerData))]
    public class LoggingHandler : IExceptionHandler
    {
        private readonly string defaultTitle;

        private readonly string logCategory;

        private readonly Type formatterType;

        public LoggingHandler(string title, string logCategory, Type formatterType)
        {
            this.defaultTitle = title;
            this.logCategory = logCategory;
            this.formatterType = formatterType;
        }

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            var entry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry(exception.Message, logCategory, 0, 100, TraceEventType.Error, defaultTitle, null);

            foreach (DictionaryEntry dataEntry in exception.Data)
            {
                if (dataEntry.Key is string)
                {
                    entry.ExtendedProperties.Add(dataEntry.Key as string, dataEntry.Value);
                }
            }

            Type[] types = new Type[] { typeof(TextWriter), typeof(Exception), typeof(Guid) };
            ConstructorInfo constructor = formatterType.GetConstructor(types);

            using (StringWriter writer = new StringWriter())
            {
                var exceptionFormatter = (ExceptionFormatter)constructor.Invoke(new object[] { writer, exception, handlingInstanceId });
                exceptionFormatter.Format();
                entry.AddErrorMessage(writer.GetStringBuilder().ToString());
            }

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(entry);

            return exception;
        }
    }
}