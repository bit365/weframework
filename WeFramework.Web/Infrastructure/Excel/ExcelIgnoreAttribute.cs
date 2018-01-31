using System;

namespace WeFramework.Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ExcelIgnoreAttribute : Attribute
    {

    }
}
