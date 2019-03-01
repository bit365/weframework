using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WeFramework.Web.Exceptions
{
    public class ErrorMessageHandlerData: ExceptionHandlerData
    {
        private const string errorMessageProperty = "errorMessage";

        [ConfigurationProperty(errorMessageProperty, IsRequired = false)]
        public string ExceptionMessage
        {
            get { return (string)this[errorMessageProperty]; }
            set { this[errorMessageProperty] = value; }
        }

        public override IExceptionHandler BuildExceptionHandler()
        {
            return new ErrorMessageHandler(ExceptionMessage);
        }
    }
}