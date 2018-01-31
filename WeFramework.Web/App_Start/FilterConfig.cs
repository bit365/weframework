using System.Web.Mvc;
using WeFramework.Web.Core.Security;

namespace WeFramework.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Exceptions.ExceptionHandlingAttribute());
        }
    }
}
