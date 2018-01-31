using System.Collections.Generic;
using System.Web.Mvc;
using WeFramework.Web.Core.Mvc;

namespace WeFramework.Web.Infrastructure
{
    public class ExcelController : BaseController
    {
        public ExcelFileResult<T> Excel<T>(IEnumerable<T> model) where T : class
        {
            return new ExcelFileResult<T>(model);
        }

        public ExcelFileResult<T> Excel<T>(IEnumerable<T> model, string fileName) where T : class
        {
            return new ExcelFileResult<T>(model, fileName);
        }
    }
}
