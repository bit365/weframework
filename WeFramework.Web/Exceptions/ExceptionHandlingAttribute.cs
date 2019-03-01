using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeFramework.Web.Exceptions
{
    public class ExceptionHandlingAttribute : HandleErrorAttribute
    {
        public string ExceptionPolicyName { get; private set; }

        public ExceptionHandlingAttribute(string exceptionPolicyName = "defaultPolicy")
        {
            this.ExceptionPolicyName = exceptionPolicyName;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            // If custom errors are disabled, we need to let the normal ASP.NET exception handler
            // execute so that the user can see useful debugging information.
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            string errorMessage = string.Empty;
            Exception exceptionToThrow = null;

            try
            {
                ExceptionPolicy.HandleException(filterContext.Exception, ExceptionPolicyName, out exceptionToThrow);
                errorMessage = HttpContext.Current.GetErrorMessage();
            }
            finally
            {
                HttpContext.Current.ClearErrorMessage();
            }

            exceptionToThrow = exceptionToThrow ?? filterContext.Exception;
            errorMessage = string.IsNullOrEmpty(errorMessage) ? exceptionToThrow.Message : errorMessage;

            string handleErrorAction = string.Format("On{0}Error", filterContext.RouteData.GetRequiredString("action"));

            filterContext.Controller.ViewData.ModelState.AddModelError(string.Empty, errorMessage);

            Controller controller = filterContext.Controller as Controller;

            if (controller != null && controller.ActionInvoker.InvokeAction(filterContext, handleErrorAction))
            {
                filterContext.ExceptionHandled = true;
            }

            base.OnException(filterContext);
        }
    }
}