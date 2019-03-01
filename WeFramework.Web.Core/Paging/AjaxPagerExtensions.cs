using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using WeFramework.Core.Paging;

namespace WeFramework.Web.Core.Paging
{
    public static class AjaxPagerExtensions
    {
        public static HtmlString AjaxPager(this HtmlHelper htmlHelper, IPagedList list, Func<int, string> generatePageUrl, AjaxOptions ajaxOptions, PagerOptions pagerOptions = null)
        {
            pagerOptions = pagerOptions ?? new PagerOptions();
            return PagerExtensions.Pager(htmlHelper, list, generatePageUrl, PagerOptions.EnableUnobtrusiveAjaxReplacing(pagerOptions,ajaxOptions));
        }
    }
}
