using FluentValidation.Mvc;
using System.Web.Mvc;
using WeFramework.Web.Validator;
using System.Linq;
using WeFramework.Web.Infrastructure;
using System.IO;
using System;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WeFramework.Web.App_Start.EntLibActivator), "Start")]
namespace WeFramework.Web.App_Start
{
    public static class EntLibActivator
    {
        public static void Start()
        {
            string configurationFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EntLib.config");
            IConfigurationSource configurationSource = new FileConfigurationSource(configurationFilepath);

            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(logWriterFactory.Create());

            ExceptionPolicyFactory exceptionPolicyFactory = new ExceptionPolicyFactory(configurationSource);
            ExceptionPolicy.SetExceptionManager(exceptionPolicyFactory.CreateManager());
        }
    }
}
