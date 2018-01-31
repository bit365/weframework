using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeFramework.Web.Exceptions
{
    [ConfigurationElementType(typeof(ErrorMessageHandlerData))]
    public class ErrorMessageHandler : IExceptionHandler
    {
        public string ErrorMessage { get; private set; }

        public ErrorMessageHandler(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.SetErrorMessage(this.ErrorMessage);
            }
            return exception;
        }
    }
}