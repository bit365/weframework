using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WeFramework.Web.App_Start.ErrorHandlingActivator), "Start")]
namespace WeFramework.Web.App_Start
{

    public static class ErrorHandlingActivator
    {
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(ErrorHandlingStartupModule));
        }

        public class ErrorHandlingStartupModule : IHttpModule
        {
            public void Init(HttpApplication context)
            {
                context.Error += new EventHandler(Application_Error);
            }

            private void Application_Error(object sender, EventArgs e)
            {
                HttpApplication httpApplication = sender as HttpApplication;
                Exception exception = httpApplication.Server.GetLastError();
                HttpException httpException = new HttpException(null, exception);

                if (httpException.GetHttpCode() == (int)HttpStatusCode.NotFound)
                {
                    RedirectToErrorAction(httpApplication.Context, HttpStatusCode.NotFound.ToString());
                }

                if (httpException.GetHttpCode() == (int)HttpStatusCode.Forbidden)
                {
                    RedirectToErrorAction(httpApplication.Context, HttpStatusCode.Forbidden.ToString());
                }
            }

            private void RedirectToErrorAction(HttpContext context, string actionName)
            {
                context.Server.ClearError();
                context.Response.TrySkipIisCustomErrors = true;
                context.Response.Clear();
                RouteData routeData = new RouteData();
                routeData.Values.Add("controller", "error");
                routeData.Values.Add("action", actionName);
                IController errorController = new Controllers.ErrorController();
                var httpContextWrapper = new HttpContextWrapper(context);
                var requestContext = new RequestContext(httpContextWrapper, routeData);
                errorController.Execute(requestContext);
            }

            public void Dispose() { }
        }
    }
}