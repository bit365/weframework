using System.IO;
using System;
using System.Configuration;
using System.Collections.Specialized;
using WeFramework.Web.Quartz;
using Unity.Lifetime;
using WeFramework.Core.Infrastructure;
using Quartz;
using Quartz.Logging;
using WeFramework.Core.Configuration;
using Unity;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WeFramework.Web.App_Start.QuartzActivator), "Start", Order = 1)]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(WeFramework.Web.App_Start.QuartzActivator), "Shutdown")]
namespace WeFramework.Web.App_Start
{
    public static class QuartzActivator
    {
        public static void Start()
        {
            string configurationFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Quartz.config");

            var configurationFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configurationFilepath };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(configurationFileMap, ConfigurationUserLevel.None);
            System.Xml.XmlDocument sectionXmlDocument = new System.Xml.XmlDocument();
            sectionXmlDocument.Load(new StringReader(configuration.GetSection("quartz").SectionInformation.GetRawXml()));
            NameValueSectionHandler handler = new NameValueSectionHandler();

            var quartzProperties = (NameValueCollection)handler.Create(null, null, sectionXmlDocument.DocumentElement);

            LogProvider.SetCurrentLogProvider(new Scheduler.EntLibLogProvider());

            var logEntiry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry();
            logEntiry.Title = "Scheduler Event";
            logEntiry.Severity = System.Diagnostics.TraceEventType.Start;
            logEntiry.Message = "Restart the scheduler has been completed";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(logEntiry);

            IUnityContainer unityContainer = new UnityContainer();
            ConfigUnityContainer(unityContainer);
            unityContainer.AddExtension(new QuartzUnityExtension(quartzProperties));
            unityContainer.Resolve<IScheduler>().Start();
        }

        public static void Shutdown()
        {
            ServiceContainer.Current.Resolve<IScheduler>().Shutdown();

            var logEntiry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry();
            logEntiry.Title = "Scheduler Event";
            logEntiry.Severity = System.Diagnostics.TraceEventType.Stop;
            logEntiry.Message = "The scheduler has stopped";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(logEntiry);
        }

        private static void ConfigUnityContainer(IUnityContainer unityContainer)
        {
            string configurationFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Quartz.Unity.config");
            var configurationFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configurationFilePath };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(configurationFileMap, ConfigurationUserLevel.None);
            var section = (Microsoft.Practices.Unity.Configuration.UnityConfigurationSection)configuration.GetSection("unity");
            section.Configure(unityContainer);

            var config = ConfigurationManager.GetSection("applicationConfig") as ApplicationConfig;
            unityContainer.RegisterInstance<ApplicationConfig>(config);

        }
    }
}