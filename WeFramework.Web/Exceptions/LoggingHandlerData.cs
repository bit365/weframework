using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WeFramework.Web.Exceptions
{
    public class LoggingHandlerData : ExceptionHandlerData
    {
        private static readonly AssemblyQualifiedTypeNameConverter typeConverter = new AssemblyQualifiedTypeNameConverter();

        private const string title = "title";

        private const string logCategory = "logCategory";

        private const string formatterType = "formatterType";

        [ConfigurationProperty(title, IsRequired = false)]
        public string Title
        {
            get { return (string)this[title]; }
            set { this[title] = value; }
        }

        [ConfigurationProperty(logCategory, IsRequired = true)]
        public string LogCategory
        {
            get { return (string)this[logCategory]; }
            set { this[logCategory] = value; }
        }

        public Type FormatterType
        {
            get { return (Type)typeConverter.ConvertFrom(FormatterTypeName); }
            set { FormatterTypeName = typeConverter.ConvertToString(value); }
        }

        [ConfigurationProperty(formatterType, IsRequired = true)]
        public string FormatterTypeName
        {
            get { return (string)this[formatterType]; }
            set { this[formatterType] = value; }
        }

        public override IExceptionHandler BuildExceptionHandler()
        {
            return new LoggingHandler(Title, LogCategory, FormatterType);
        }
    }
}