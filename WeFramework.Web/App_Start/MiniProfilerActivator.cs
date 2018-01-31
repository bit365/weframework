using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WeFramework.Web.App_Start.MiniProfilerActivator), "Start")]
namespace WeFramework.Web.App_Start
{
    public static class MiniProfilerActivator
    {
        public static void Start()
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["MiniProfilerEnabled"]))
            {
                StackExchange.Profiling.MiniProfiler.Settings.CustomUITemplates = @"~/Extensions/MiniProfiler";

                StackExchange.Profiling.EntityFramework6.MiniProfilerEF6.Initialize();

                DynamicModuleUtility.RegisterModule(typeof(MiniProfilerStartupModule));
                GlobalFilters.Filters.Add(new StackExchange.Profiling.Mvc.ProfilingActionFilter());
                List<IViewEngine> viewEngines = ViewEngines.Engines.ToList();
                ViewEngines.Engines.Clear();
                foreach (IViewEngine viewEngine in viewEngines)
                {
                    ViewEngines.Engines.Add(new StackExchange.Profiling.Mvc.ProfilingViewEngine(viewEngine));
                }
            }
        }

        public class MiniProfilerStartupModule : IHttpModule
        {
            public void Init(HttpApplication context)
            {
                context.BeginRequest += (sender, e) =>  StackExchange.Profiling.MiniProfiler.Start();

                context.EndRequest += (sender, e) => StackExchange.Profiling.MiniProfiler.Stop();
            }

            public void Dispose() { }
        }
    }
}