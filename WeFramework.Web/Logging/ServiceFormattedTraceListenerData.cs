using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using System.Configuration;
using System.Diagnostics;

namespace WeFramework.Web.Logging
{
    public class ServiceFormattedTraceListenerData : TraceListenerData
    {
        private const string formatterNameProperty = "formatter";

        [ConfigurationProperty(formatterNameProperty, IsRequired = false)]
        public string Formatter
        {
            get { return (string)base[formatterNameProperty]; }
            set { base[formatterNameProperty] = value; }
        }

        protected override TraceListener CoreBuildTraceListener(LoggingSettings settings)
        {
            var formatter = this.BuildFormatterSafe(settings, this.Formatter);
            return new ServiceFormattedTraceListener(formatter);
        }
    }
}